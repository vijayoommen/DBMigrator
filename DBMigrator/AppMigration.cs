using FluentMigrator;
using FluentMigrator.Runner;
using FluentMigrator.Runner.Logging;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;

namespace DBMigrator
{
    public class AppMigration : Migration
    {
        private readonly ILogger _logger;

        public AppMigration()
        {
            _logger = AppMigrationRunner.Scope.ServiceProvider.GetRequiredService<ILoggerFactory>().CreateLogger(typeof(FluentMigratorConsoleLogger));
        }

        public override void Down()
        {
            Down(AppMigrationRunner.Options.Preview);
        }

        public override void Up()
        {
            Up(AppMigrationRunner.Options.Preview);

        }

        public virtual void Down(bool isPreviewMode) {
            throw new NotImplementedException();
        }
        public virtual void Up(bool isPrevewMode) {
            throw new NotImplementedException();
        }
    }
}
