using System;
using FluentMigrator.Runner;
using System.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DBMigrator
{
    public class MigrationRunner
    {
        internal static void MigrateUp(long version = 0)
        {
            Console.WriteLine("********************Starting to migrate up...");
            var serviceProvider = CreateServices();

            // Put the database update into a scope to ensure
            // that all resources will be disposed.
            using (var scope = serviceProvider.CreateScope())
            {
                // Instantiate the runner
                var runner = serviceProvider.GetRequiredService<IMigrationRunner>();

                using(var runnerScope = runner.BeginScope())
                {
                    try
                    {
                        if (version > 0)
                            runner.MigrateUp(version);
                        else
                            runner.MigrateUp();

                        runnerScope.Complete();
                    }
                    catch(Exception ex)
                    {
                        runnerScope.Cancel();
                    }
                }
            }
        }

        internal static void ListMigrations()
        {
            var serviceProvider = CreateServices();

            // Put the database update into a scope to ensure
            // that all resources will be disposed.
            using (var scope = serviceProvider.CreateScope())
            {
                // Instantiate the runner
                var runner = serviceProvider.GetRequiredService<IMigrationRunner>();

                // Execute the migrations
                runner.ListMigrations();
            }
        }

        internal static void Preview()
        {
            var serviceProvider = CreateServices();

            Console.WriteLine("*********** RUNNING IN PREVIEW MODE ONLY.");
            using (var scope = serviceProvider.CreateScope())
            {
                // Instantiate the runner
                var runner = serviceProvider.GetRequiredService<IMigrationRunner>();

                // begin a tranasction
                using (var runnerScope = runner.BeginScope())
                {
                    try
                    {
                        // Execute the migrations
                        runner.MigrateUp();
                    }
                    finally
                    {
                        // cancel - and check to see if there are errors
                        runnerScope.Cancel();
                    }
                }
            }
            Console.WriteLine("*********** ALL CHANGES ROLLBACK.");
        }

        internal static void MigrateDown(long version)
        {
            Console.WriteLine($"********Rollback initiated......{version}");
            var serviceProvider = CreateServices();

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
                    .ConfigureGlobalProcessorOptions(options => { options.PreviewOnly = false;  })
                    )
                // Enable logging to console in the FluentMigrator way
                .AddLogging(lb => lb.AddFluentMigratorConsole())
                // Build the service provider
                .BuildServiceProvider(false);
        }

    }

}
