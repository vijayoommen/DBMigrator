using FluentMigrator;

namespace DBMigrator
{
    public class AppMigrationAttribute : MigrationAttribute
    {
        public AppMigrationAttribute(long version) :  base(version, (AppMigrationRunner.Options.Preview ? TransactionBehavior.None : TransactionBehavior.Default)) { }
    }
}
