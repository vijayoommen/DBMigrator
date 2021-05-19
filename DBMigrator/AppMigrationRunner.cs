using System;
using FluentMigrator.Runner;
using System.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DBMigrator
{
    public class AppMigrationRunner
    {
        private static IServiceProvider _appServiceProvider;
        internal static IServiceProvider AppServiceProvider
        {
            get
            {
                if (_appServiceProvider == null)
                    _appServiceProvider = CreateServices();
                return _appServiceProvider;
            }
        }

        public static AppOptions Options { get; private set; }
        public static IServiceScope Scope { get; private set; }
        public static IMigrationRunner MigrationRunner { get; private set; }
        

        internal static void MigrateUp(AppOptions options)
        {
            Console.WriteLine("********************Starting to migrate up...");
            Options = options;
            using (Scope = AppServiceProvider.CreateScope())
            {
                MigrationRunner = Scope.ServiceProvider.GetRequiredService<IMigrationRunner>();
                MigrationRunner.MigrateUp();
            }
        }

        internal static void ListMigrations()
        {
            Options = new AppOptions();
            var serviceProvider = AppServiceProvider;

            // Put the database update into a scope to ensure
            // that all resources will be disposed.
            using (Scope = serviceProvider.CreateScope())
            {
                // Instantiate the runner
                MigrationRunner = serviceProvider.GetRequiredService<IMigrationRunner>();

                // Execute the migrations
                MigrationRunner.ListMigrations();
            }
        }

        internal static void Preview(AppOptions options)
        {
            Console.WriteLine("**************** Starting preview mode...");
            Options = options;

            using (Scope = AppServiceProvider.CreateScope())
            {
                MigrationRunner = Scope.ServiceProvider.GetRequiredService<IMigrationRunner>();
                using (var runnerScope = MigrationRunner.BeginScope())
                {
                    try
                    {
                        MigrationRunner.MigrateUp();
                    }
                    finally
                    {
                        runnerScope.Cancel();
                    }
                }
            }
        }

        internal static void MigrateDown(AppOptions options)
        {
            Console.WriteLine($"********Rollback initiated......{options.Version}");
            Options = options;
            var version = Options.Version;
            var serviceProvider = AppServiceProvider;

            // Put the database update into a scope to ensure
            // that all resources will be disposed.
            using (var scope = serviceProvider.CreateScope())
            {
                // Instantiate the runner
                var runner = serviceProvider.GetRequiredService<IMigrationRunner>();

                Console.WriteLine($"{version} can rollback {runner.HasMigrationsToApplyDown(version)}");

                // Execute the migrations
                runner.MigrateDown(version);
            }
        }

        /// <summary>
        /// Configure the dependency injection services
        /// </summary>
        private static IServiceProvider CreateServices()
        {
            return new ServiceCollection()
                // Add common FluentMigrator services
                .AddFluentMigratorCore()
                .ConfigureRunner(rb => rb
                    // Add the right version of SQL Server Runner
                    .AddSqlServer2014()
                    // Set the connection string
                    .WithGlobalConnectionString(ConfigurationManager.ConnectionStrings["db"].ConnectionString)
                    // Define the assembly containing the migrations
                    .ScanIn(typeof(DBMigrator.Program).Assembly).For.All()
                    .ConfigureGlobalProcessorOptions(options =>
                    {
                        options.PreviewOnly = false;
                    })
                )
                // Enable logging to console in the FluentMigrator way
                .AddLogging(lb => lb.AddFluentMigratorConsole())
                // Build the service provider
                .BuildServiceProvider(false);
        }
    }
}
