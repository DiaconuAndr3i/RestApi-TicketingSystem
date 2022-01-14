using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TicketingSystem_Helpdesk
{
    public class AuthorizationServices
    {
        public static IServiceCollection AddAuthorizationServices(IServiceCollection services, 
            IConfiguration configuration)
        {
            var guest = configuration
                .GetSection("Roles")
                .GetSection("DefaultRole")
                .Get<string>();
            var admin = configuration
                .GetSection("Roles")
                .GetSection("HandlerControls")
                .GetSection("AdminRole")
                .Get<string>();
            var dean = configuration
                .GetSection("Roles")
                .GetSection("UserControls")
                .GetSection("DeanRole")
                .Get<string>();
            var student = configuration
                .GetSection("Roles")
                .GetSection("UserControls")
                .GetSection("StudentRole")
                .Get<string>();
            var secretBuiss = configuration
                .GetSection("Roles")
                .GetSection("UserControls")
                .GetSection("SecretaryAdmBuisnessRole")
                .Get<string>();
            var secretMath = configuration
                .GetSection("Roles")
                .GetSection("UserControls")
                .GetSection("SecretaryMathRole")
                .Get<string>();
            var secretComSci = configuration
                .GetSection("Roles")
                .GetSection("UserControls")
                .GetSection("SecretaryCompScience")
                .Get<string>();
            var secretComInfTeh = configuration
                .GetSection("Roles")
                .GetSection("UserControls")
                .GetSection("SecretaryComInfTehn")
                .Get<string>();

            services.AddAuthorization(options =>
            {
                options.AddPolicy("Admin", policy => policy
                .RequireRole(admin).RequireAuthenticatedUser()
                .AddAuthenticationSchemes("AuthScheme").Build());
                options.AddPolicy("Student", policy => policy
                .RequireRole(student).RequireAuthenticatedUser()
                .AddAuthenticationSchemes("AuthScheme").Build());
                options.AddPolicy("AllRolesWithoutGuest", policy => policy
                .RequireRole(admin, dean, student, secretBuiss, secretMath, secretComSci, secretComInfTeh).RequireAuthenticatedUser()
                .AddAuthenticationSchemes("AuthScheme").Build());
                options.AddPolicy("AllRoles", policy => policy
                .RequireRole(admin, dean, student, secretBuiss, secretMath, secretComSci, secretComInfTeh, guest).RequireAuthenticatedUser()
                .AddAuthenticationSchemes("AuthScheme").Build());
            }
            );

            return services;
        }
    }
}
