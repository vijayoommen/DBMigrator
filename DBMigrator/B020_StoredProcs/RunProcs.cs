using FluentMigrator;
using System;
using System.Linq;

namespace DBMigrator.B020_StoredProcs
{
    [AppMaintenance(MigrationStage.AfterAll)]
    public class RunProcs : AppMigration
    {
        public override void Down()
        {
            throw new NotImplementedException();
        }

        public override void Up()
        {
            var resources = Helper.FindScripts(this.GetType().Namespace);
            resources.ToList().ForEach(script =>
            {
                RunDropStatement(script);
                Execute.EmbeddedScript(script);
            });
        }

        private void RunDropStatement(string script)
        {
            var objectName = script.Replace($"{typeof(RunProcs).Namespace}.EmbeddedScripts.", "").ToLower().Replace(".sql", "");
            var sql = $@"drop proc if exists {objectName}";
            Execute.Sql(sql);
        }
    }
}
