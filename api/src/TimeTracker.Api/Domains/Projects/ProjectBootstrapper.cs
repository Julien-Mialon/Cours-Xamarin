using TimeTracker.Api.Domains.Projects.Services;

namespace TimeTracker.Api.Domains.Projects;

public static class ProjectBootstrapper
{
    public static IServiceCollection UseProjectModule(this IServiceCollection services)
    {
        services.AddScoped<IProjectService, ProjectService>()
            .AddScoped<IProjectTaskService, ProjectTaskService>()
            .AddScoped<ITimeService, TimeService>()
            ;
			
        return services;
    }
}