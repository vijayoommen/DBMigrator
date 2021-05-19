using FluentMigrator.Runner;
using System;
using System.Collections.Generic;
using System.Text;

namespace DBMigrator.A010_DBScripts
{
    [AppMigration(20190519002)]
    public class M0004_CreateMetricsTable : AppMigration
    {
        public override void Up(bool isPrevewMode)
        {
            Create.Table("Metrics").InSchema("dbo")
                .WithColumn("Id").AsInt32().NotNullable().Identity()
                .WithColumn("ProcessId").AsInt32().NotNullable()
                .WithColumn("StartTime").AsInt32()
                .WithColumn("UpdatedOn").AsDateTime().WithDefault(FluentMigrator.SystemMethods.CurrentDateTime);
        }
    }
}
