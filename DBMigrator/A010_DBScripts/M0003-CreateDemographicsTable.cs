using FluentMigrator.Runner;
using System;
using System.Collections.Generic;
using System.Text;

namespace DBMigrator.A010_DBScripts
{
    [AppMigration(20210519001)]
    public class M0003_CreateDemographicsTable : AppMigration
    {
        public override void Up(bool isPrevewMode)
        {
            Create.Table("Demotraphics").InSchema("dbo")
                .WithColumn("Id").AsInt32().NotNullable().Identity()
                .WithColumn("Age").AsInt32()
                .WithColumn("Gender").AsString()
                .WithColumn("State").AsString()
                .WithColumn("CreatedOn").AsDateTime().WithDefault(FluentMigrator.SystemMethods.CurrentDateTime);
        }
    }
}
