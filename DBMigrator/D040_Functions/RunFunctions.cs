using FluentMigrator;
using System;
using System.Linq;

namespace DBMigrator.D040_Functions
{
    [Maintenance(MigrationStage.BeforeAll, TransactionBehavior.None)]
    public class RunFunctions : Migration
    {
        public override void Down()
        {
            throw new NotImplementedException();
        }

        public override void Up()
        {
            var resources = Utilities.FindScripts(this.GetType().Namespace);
            resources.ToList().ForEach(script => Execute.EmbeddedScript(script));
        }
    }
}
