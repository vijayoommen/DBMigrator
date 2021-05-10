using FluentMigrator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DBMigrator.C030_Views
{
    [Maintenance(MigrationStage.BeforeAll, TransactionBehavior.None)]
    public class RunViews : Migration
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
