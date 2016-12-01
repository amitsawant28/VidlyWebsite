namespace Vidly.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddingPhoneNumberToForm : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "MobileNumber", c => c.String(nullable: false, maxLength: 10));
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "MobileNumber");
        }
    }
}
