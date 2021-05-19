using FluentMigrator;
using FluentMigrator.Runner;
using System;

namespace DBMigrator.B020_StoredProcs.EmbeddedScripts
{
    [AppMaintenance(MigrationStage.AfterAll)]
    public class SeedData : AppMigration
    {
        public override void Down(bool isPrevewMode)
        {

        }

        public override void Up(bool isPrevewMode)
        {
            var rand = new Random().Next(1, 10000);
            var email = $"test+{rand}@email.com";
            Console.WriteLine($"Expect {email}");
            Execute.Sql($"insert into AppUser (Name, Dob, Email, isLocked, isActive) values ('t', getDate(), 'Ex+{email}', 1, 0)");
        }
    }
}
