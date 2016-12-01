namespace Vidly.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SeedUsers : DbMigration
    {
        public override void Up()
        {
            Sql(@"
                INSERT INTO [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (N'3a8943b5-15a9-48fb-b8e0-59ba25cbdee3', N'admin@vidly.com', 0, N'AAjJCGB0oNgU31bN8EvK/+6B5LYBRNsOyRpQHSSb3npBXxPZp3MtoXTAHvpnhs5OqQ==', N'5d6ef38c-1635-4546-8d40-89b9d96fb0d9', NULL, 0, 0, NULL, 0, 0, N'admin@vidly.com')
                INSERT INTO [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (N'd95019aa-9b43-4ecc-9820-b86685dbab75', N'guest@vidly.com', 0, N'AN8fRppg8tdd9K05H1GDbK8YP1JJ+vpElq/JpJTmPRdtLg44koUfO2RVFcNo6yNWgQ==', N'8306bc21-bdf3-480f-969e-70161bece3c6', NULL, 0, 0, NULL, 0, 0, N'guest@vidly.com')
                
                INSERT INTO [dbo].[AspNetRoles] ([Id], [Name]) VALUES (N'ab4feda2-4b92-4313-81c2-bbb6bc1e0a90', N'Admin')
                
                INSERT INTO [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'3a8943b5-15a9-48fb-b8e0-59ba25cbdee3', N'ab4feda2-4b92-4313-81c2-bbb6bc1e0a90')
            ");
        }
        
        public override void Down()
        {
        }
    }
}
