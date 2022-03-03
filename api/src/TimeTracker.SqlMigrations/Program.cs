using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Storm.Api.Configurations;
using Storm.Api.Core.Databases;
using Storm.SqlMigrations;

namespace TimeTracker.SqlMigrations;

public class Program
{
    private static readonly List<IMigrationModule> Modules = new List<IMigrationModule>()
    {
        new TimeTrackerMigrations()
    };
        
    public static async Task<int> Main(string[] args)
    {
        if (args.Length == 0)
        {
            Console.WriteLine("Usage: TimeTracker.SqlMigrations <environment>");
            return -42;
        }
            
        IConfiguration configuration = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
            .AddJsonFile($"appsettings.{args[0]}.json", optional: false, reloadOnChange: false)
            .Build();

        IServiceCollection services = new ServiceCollection();
        services.AddDatabaseModule(configuration.GetSection("Database").LoadDatabaseConfiguration());
        IServiceProvider providers = services.BuildServiceProvider();
            
        using (providers.CreateScope())
        {
            MigrationEngine migrations = new MigrationEngine(providers, Modules);
            return await migrations.Run() ? 0 : -1;
        }
    }
}