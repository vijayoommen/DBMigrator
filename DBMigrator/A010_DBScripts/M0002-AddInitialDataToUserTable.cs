using FluentMigrator;
using System;
using System.Collections.Generic;
using System.Text;

namespace DBMigrator.A010_DBScripts
{
    [Migration(1002)]
    public class M0002_AddInitialDataToUserTable : Migration
    {
        public override void Down()
        {
            Execute.Sql("delete from dbo.AppUser where NAme in ( 'Jack', 'Jill')");
        }

        public override void Up()
        {
            Execute.Sql(@"Insert into dbo.AppUser (Name, DOB, Email, IsLocked, IsActive) VALUES 
                            ('Jack', getdate(), 'jack@email.com', 0, 1),
                            ('Jill', getdate(), 'jill@email.com', 0, 1)");
        }
    }
}
