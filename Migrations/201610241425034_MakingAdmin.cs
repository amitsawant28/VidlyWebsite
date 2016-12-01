namespace Vidly.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MakingAdmin : DbMigration
    {
        public override void Up()
        {
            Sql("UPDATE AspNetRoles SET Name='Admin' WHERE Id='ab4feda2-4b92-4313-81c2-bbb6bc1e0a90'");
        }
        
        public override void Down()
        {
        }
    }
}
