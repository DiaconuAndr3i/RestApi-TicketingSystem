﻿using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TicketingSystem_Helpdesk.Entities;
using TicketingSystem_Helpdesk.Models;
using TicketingSystem_Helpdesk.Repositories;

namespace TicketingSystem_Helpdesk.Managers
{
    public class AuthentificationManager : IAuthentificationManager
    {
        private readonly UserManager<User> userManager;
        private readonly SignInManager<User> signInManager;
        private readonly ITokenManager tokenManager;
        private readonly IMyUserManager myUserManager;
        private readonly IConfiguration configuration;
        private readonly IServicesManager servicesManager;
        private readonly IGuestManager guestManager;
        private readonly IInstitutionManager institutionManager;
        private readonly IRoleManager roleManager;
        private readonly IMyUserRepository myUserRepository;


        public AuthentificationManager(UserManager<User> userManager, 
            SignInManager<User> signInManager, ITokenManager tokenManager,
            IMyUserManager myUserManager, IConfiguration configuration,
            IServicesManager servicesManager, IGuestManager guestManager,
            IInstitutionManager institutionManager,
            IRoleManager roleManager,
            IMyUserRepository myUserRepository)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.tokenManager = tokenManager;
            this.myUserManager = myUserManager;
            this.configuration = configuration;
            this.servicesManager = servicesManager;
            this.guestManager = guestManager;
            this.institutionManager = institutionManager;
            this.roleManager = roleManager;
            this.myUserRepository = myUserRepository;
        }
        public async Task<LoginResponseModel> Login(LoginModel loginModel)
        {
            var user = await userManager.FindByEmailAsync(loginModel.Email);
            if (servicesManager.ValidationExistUser(user))
            {
                var result = await signInManager.CheckPasswordSignInAsync(user, loginModel.Password, false);
                if (servicesManager.ValidationSignInResult(result))
                {
                    var roles = await myUserManager.GetUserRolesByInstitution(user, loginModel.Institution);
                    if (!servicesManager.ValidationCountZero(roles))
                    {
                        return null;
                    }

                    var accessToken = tokenManager.GenerateAccessToken(roles);
                    var refreshToken = tokenManager.GenerateRefreshToken();

                    user.RefreshTokens.Add(refreshToken);

                    myUserRepository.UpdateUser(user);


                    return new LoginResponseModel
                    {
                        FirstName = user.First_name,
                        Lastname = user.Last_name,
                        AccessToken = accessToken,
                        RefreshToken = refreshToken.Token,
                        Roles = roles,
                        Institution = loginModel.Institution
                    };
                }
            }

            return null;
        }

        public async Task<string> Register(RegisterModel registerModel)
        {
            var existUser = await userManager.FindByEmailAsync(registerModel.Email);
            var defaultRole = configuration.GetSection("Roles").GetSection("DefaultRole").Get<string>();
            if (servicesManager.ValidationExistUser(existUser))
            {
                var existUserRoles = await myUserManager.GetAllUserRoles(existUser);
                var existUserRolesName = roleManager.GetNameUserRoles(existUserRoles);

                if (servicesManager.ValidationExistElement(existUserRolesName,defaultRole))
                {
                    return configuration.GetSection("RegisterStatus").GetSection("StatusGuest").Get<string>();
                }
                return configuration.GetSection("RegisterStatus").GetSection("StatusAccountExist").Get<string>();
            }


            if (!servicesManager.ValidationExistElement(await roleManager.GetNameRoles(), registerModel.RoleName) || 
                !servicesManager.ValidationExistElement(await institutionManager.GetNameInstitutions(),registerModel.InstitutionName))
                return configuration.GetSection("RegisterStatus").GetSection("StatusProblem").Get<string>();

            var user = new User
            {
                Email = registerModel.Email,
                UserName = registerModel.Email,
                PhoneNumber = registerModel.Phone_number,
                First_name = registerModel.First_name,
                Last_name = registerModel.Last_name
            };

            var result = await userManager.CreateAsync(user, registerModel.Password);

            if (servicesManager.ValidationIdentityResul(result))
            {
                await myUserManager.AddToUserRole(new HandleUserRoleModel
                {
                    Email = user.Email,
                    RoleName = defaultRole,
                    InstitutionName = registerModel.InstitutionName
                });

                await guestManager.RegisterGuest(user, registerModel.RoleName);

                return configuration.GetSection("RegisterStatus").GetSection("StatusOk").Get<string>();
            }

            return configuration.GetSection("RegisterStatus").GetSection("StatusProblem").Get<string>();
        }

        public async Task<AccessTokenModel> RefreshToken(RefreshTokenModel refreshTokenModel)
        {
            var user = myUserRepository.GetAllUsers().SingleOrDefault(u => u.RefreshTokens.Any(t => t.Token == refreshTokenModel.RefreshToken));

            if (user == null)            
                return null;

            var refreshToken = user.RefreshTokens.Single(x => x.Token == refreshTokenModel.RefreshToken);

            if (!refreshToken.IsActive) 
                return null;

            var roles = await myUserManager.GetUserRolesByInstitution(user, refreshTokenModel.Institution);
            
            if (!servicesManager.ValidationCountZero(roles))
                return null; 

            var accessToken = tokenManager.GenerateAccessToken(roles);

            return new AccessTokenModel
            {
                AccessToken = accessToken
            };
        }
    }
}
