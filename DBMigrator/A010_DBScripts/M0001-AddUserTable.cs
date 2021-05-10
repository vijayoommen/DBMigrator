using FluentMigrator;
using FluentMigrator.Infrastructure;

namespace DBMigrator._001_Migrations
{
    [Migration(1001, TransactionBehavior.None)]
    public class M0001_AddUserTable : Migration
    {
        public override void Down()
        {
            Delete.Table("AppUser").InSchema("dbo");
        }

        public override void Up()
        {
            Create.Table("AppUser").InSchema("dbo")
                .WithColumn("Id").AsInt32().NotNullable().Identity()
                .WithColumn("Name").AsString().NotNullable()
                .WithColumn("DOB").AsDateTime2().NotNullable()
                .WithColumn("Email").AsString().NotNullable()
                .WithColumn("IsLocked").AsBoolean().WithDefaultValue(0)
                .WithColumn("IsActive").AsBoolean().WithDefaultValue(1);
        }
    }
}
