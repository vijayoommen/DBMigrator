using FluentMigrator;

namespace DBMigrator
{
    public class AppMaintenanceAttribute : MaintenanceAttribute
    {
        public AppMaintenanceAttribute(MigrationStage stage) : base(stage, AppMigrationRunner.Options.Preview ? TransactionBehavior.None : TransactionBehavior.Default) { }
    }
}
