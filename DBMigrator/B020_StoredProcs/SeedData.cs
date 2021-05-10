using FluentMigrator;
using System;
using System.Collections.Generic;
using System.Text;

namespace DBMigrator.B020_StoredProcs.EmbeddedScripts
{
    [Maintenance(MigrationStage.AfterAll)]
    public class SeedData : Migration
    {
        public override void Down()
        {
            
        }

        public override void Up()
        {
            var rand = new Random().Next(1, 10000);
            var email = $"test+{rand}@email.com";
            Execute.Sql($"insert into AppUser (Name, Dob, Email, isLocked, isActive) values ('t', getDate(), '{email}', 1, 0)");
        }
    }
}
