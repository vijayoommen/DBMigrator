using FluentMigrator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DBMigrator.C030_Views
{
    [AppMaintenance(MigrationStage.BeforeAll)]
    public class RunViews : Migration
    {
        public override void Down()
        {
            throw new NotImplementedException();
        }

        public override void Up()
        {
            var resources = Helper.FindScripts(this.GetType().Namespace);
            resources.ToList().ForEach(script => {
                RunDropStatement(script);
                Execute.EmbeddedScript(script);
            });
        }
        private void RunDropStatement(string script)
        {
            var objectName = script.Replace($"{typeof(RunViews).Namespace}.EmbeddedScripts.", "").ToLower().Replace(".sql", "");
            var sql = $@"drop view if exists {objectName}";
            Execute.Sql(sql);
        }
    }
}
