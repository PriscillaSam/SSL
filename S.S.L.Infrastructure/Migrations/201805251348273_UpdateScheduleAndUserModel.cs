namespace S.S.L.Infrastructure.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateScheduleAndUserModel : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "Gender", c => c.String());
            AddColumn("dbo.Users", "Bio", c => c.String());
            AddColumn("dbo.Schedules", "Status", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Schedules", "Status");
            DropColumn("dbo.Users", "Bio");
            DropColumn("dbo.Users", "Gender");
        }
    }
}
