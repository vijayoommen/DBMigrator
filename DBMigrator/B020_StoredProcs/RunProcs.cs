using FluentMigrator;
using System;
using System.Linq;

namespace DBMigrator.B020_StoredProcs
{
    [Maintenance(MigrationStage.AfterAll, TransactionBehavior.None)]
    public class RunProcs : Migration
    {
        public override void Down()
        {
            throw new NotImplementedException();
        }

        public override void Up()
        {
            var resources = Utilities.FindScripts(this.GetType().Namespace);
            // check the expcetion list
            resources.ToList().ForEach(script => Execute.EmbeddedScript(script));
        }
    }
}
