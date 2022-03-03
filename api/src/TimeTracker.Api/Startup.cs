using Storm.Api.Launchers;
using TimeTracker.Api.Domains.Accounts;
using TimeTracker.Api.Domains.Authentications;
using TimeTracker.Api.Domains.Projects;

namespace TimeTracker.Api;

public class Startup : BaseStartup
{
    protected override string LogsProjectName { get; } = "timetracker";
        
    public Startup(IConfiguration configuration, IWebHostEnvironment environment) : base(configuration, environment)
    {
        ForceHttps = false;
    }
        
    public override void ConfigureServices(IServiceCollection services)
    {
        base.ConfigureServices(services);

        services.AddSwaggerGenNewtonsoftSupport();

        RegisterConsoleLogger(services);

        services.UseAccountModule()
            .UseAuthenticationModule()
            .UseProjectModule()
            ;
    }

    public override void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        base.Configure(app, env);

        app.UseDeveloperExceptionPage();
    }
}