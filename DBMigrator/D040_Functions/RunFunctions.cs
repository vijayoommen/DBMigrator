using FluentMigrator;
using System;
using System.Linq;

namespace DBMigrator.D040_Functions
{
    [AppMaintenance(MigrationStage.BeforeAll)]
    public class RunFunctions : Migration
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
            var objectName = script.Replace($"{typeof(RunFunctions).Namespace}.EmbeddedScripts.", "").ToLower().Replace(".sql", "");
            var sql = $@"drop function if exists {objectName}";
            Execute.Sql(sql);
        }
    }
}
