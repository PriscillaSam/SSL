namespace S.S.L.Infrastructure.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateFacilitatorMenteeModels : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Schedules", "MenteeId", "dbo.Mentees");
            DropIndex("dbo.Schedules", new[] { "MenteeId" });
            AddColumn("dbo.Schedules", "UserId", c => c.Int(nullable: false));
            CreateIndex("dbo.Schedules", "UserId");
            AddForeignKey("dbo.Schedules", "UserId", "dbo.Users", "Id", cascadeDelete: true);
            DropColumn("dbo.Schedules", "MenteeId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Schedules", "MenteeId", c => c.Int(nullable: false));
            DropForeignKey("dbo.Schedules", "UserId", "dbo.Users");
            DropIndex("dbo.Schedules", new[] { "UserId" });
            DropColumn("dbo.Schedules", "UserId");
            CreateIndex("dbo.Schedules", "MenteeId");
            AddForeignKey("dbo.Schedules", "MenteeId", "dbo.Mentees", "Id", cascadeDelete: true);
        }
    }
}
