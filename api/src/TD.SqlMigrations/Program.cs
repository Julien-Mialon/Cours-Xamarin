using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Common.Api.Configurations;
using Common.Core.Database;
using Common.SqlMigrations;
using Common.SqlMigrations.Models;
using Microsoft.Extensions.Configuration;

namespace TD.SqlMigrations
{
    public class Program
    {
        private static readonly List<IMigrationModule> Modules = new List<IMigrationModule>
        {
            new TdMigrations(),
        };
        
        public static async Task<int> Main(string[] args)
        {
            if (args.Length == 0)
            {
                Console.WriteLine("Usage: TD.SqlMigrations <environment>");
                return -42;
            }

            IConfiguration configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .AddJsonFile($"appsettings.{args[0]}.json", optional: false, reloadOnChange: false)
                .Build();

            var databaseFactory = configuration.GetSection("Database").LoadDatabaseConfiguration().CreateFactory();
            OrmLiteInterceptors.Initialize();
            MigrationEngine migrations = new MigrationEngine(databaseFactory, Modules);

            return await migrations.Run(args.Length >= 2 && args[1] == "revert", args.Length >= 3 ? args[2] : null) ? 0 : -1;
        }
    }
}
