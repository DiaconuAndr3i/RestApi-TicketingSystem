using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TicketingSystem_Helpdesk.Managers;
using TicketingSystem_Helpdesk.Repositories;

namespace TicketingSystem_Helpdesk
{
    public static class TransitentServices
    {
        public static IServiceCollection AddTransitentServices(IServiceCollection services)
        {
            services.AddTransient<IAuthentificationManager, AuthentificationManager>();
            services.AddTransient<ITokenManager, TokenManager>();
            services.AddTransient<IGuestManager, GuestManager>();
            services.AddTransient<IInstitutionManager, InstitutionManager>();
            services.AddTransient<IMyUserManager, MyUserManager>();
            services.AddTransient<IRoleManager, RoleManager>();
            services.AddTransient<IServicesManager, ServicesManager>();
            services.AddTransient<ITicketManager, TicketManager>();
            services.AddTransient<IMessageManager, MessageManager>();
            services.AddTransient<IPriorityManager, PriorityManager>();
            services.AddTransient<IStatusManager, StatusManager>();
            services.AddTransient<ITagManager, TagManager>();
            services.AddTransient<IDepartmentsSubdepartmentsManager, DepartmentsSubdepartmentsManager>();
            services.AddTransient<IInstitutionDepartmentManager, InstitutionDepartmentManager>();
            services.AddTransient<IStatisticsManager, StatisticsManager>();


            services.AddTransient<IMyUserRepository, MyUserRepository>();
            services.AddTransient<IGuestRepository, GuestRepository>();
            services.AddTransient<IInstitutionRepository, InstitutionRepository>();
            services.AddTransient<IRoleRepository, RoleRepository>();
            services.AddTransient<ITicketRepository, TicketRepository>();
            services.AddTransient<IMessageRepository, MessageRepository>();
            services.AddTransient<IPriorityRepository, PriorityRepository>();
            services.AddTransient<IStatusRepository, StatusRepository>();
            services.AddTransient<ITagRepository, TagRepository>();
            services.AddTransient<IDepartmentsSubdepartmentsRepository, DepartmentsSubdepartmentsRepository>();
            services.AddTransient<IInstitutionDepartmentRepository, InstitutionDepartmentRepository>();


            return services;
        }
    }
}
