
using Microsoft.Extensions.DependencyInjection;
using CleanEvoBPM.Infrastructure.DatabaseServices;
using Microsoft.Extensions.Configuration;
using CleanEvoBPM.Application.DatabaseServices.Interfaces;
using CleanEvoBPM.Application.Common;

namespace CleanEvoBPM.Infrastructure
{
    public static class RegisterServices
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient<IProjectDataService, ProjectDataServices>();
            services.AddTransient<IProjectTypeDataService, ProjectTypeDataService>();
            services.AddTransient<IServiceTypeDataService, ServiceTypeDataService>();
            services.AddTransient<IMethodologyDataService, MethodologyDataService>();
            services.AddTransient<IBusinessDomainDataService, BusinessDomainDataService>();
            services.AddTransient<IClientDataService, ClientDataService>();
            services.AddTransient<ITechnologyDataService, TechnologyDataService>();
            services.AddTransient<IStatusDataService, StatusDataService>();
            services.AddTransient<IProjectRiskDataService, ProjectRiskDataService>();
            services.AddTransient(typeof(IGenericDataService<>),typeof(GenericDataService<>));
            services.AddTransient<IDatabaseConnectionFactory>(e => { return new SqlConnectionFactory(configuration[Configuration.ConnectionString]); });
            services.AddTransient<IProblemCategoryService, ProblemCategoryService>();
            services.AddTransient<IProjectMilestoneDataService, ProjectMilestoneDataService>();
            services.AddTransient<IProjectLLBPDataService, ProjectLLBPDataService>();
            services.AddTransient<IDeliveryODCDataService, DeliveryODCDataService>();
            services.AddTransient<IDeliveryLocationDataService, DeliveryLocationDataService>();
            services.AddTransient<IResourceService, ResourceService>();
            services.AddTransient<IRoleService, RoleService>();
            services.AddTransient<IProjectHolidayPlanDataService, ProjectHolidayPlanDataService>();
            return services;
        }
    }
}
