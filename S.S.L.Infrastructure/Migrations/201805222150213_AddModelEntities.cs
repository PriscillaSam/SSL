namespace S.S.L.Infrastructure.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddModelEntities : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Comments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Message = c.String(),
                        UserId = c.Int(nullable: false),
                        ForumId = c.Int(nullable: false),
                        CreatedAt = c.DateTime(nullable: false),
                        UpdatedAt = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Fora", t => t.ForumId, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: false)
                .Index(t => t.UserId)
                .Index(t => t.ForumId);
            
            CreateTable(
                "dbo.Fora",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        Detail = c.String(),
                        Scriptures = c.String(),
                        CreatedAt = c.DateTime(nullable: false),
                        UpdatedAt = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Facilitators",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        CreatedAt = c.DateTime(nullable: false),
                        UpdatedAt = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Mentees",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        FacilitatorId = c.Int(nullable: false),
                        CreatedAt = c.DateTime(nullable: false),
                        UpdatedAt = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Facilitators", t => t.FacilitatorId, cascadeDelete: false)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.FacilitatorId);
            
            CreateTable(
                "dbo.Schedules",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Date = c.DateTime(nullable: false),
                        Time = c.DateTime(nullable: false),
                        Detail = c.String(),
                        MenteeId = c.Int(nullable: false),
                        CreatedAt = c.DateTime(nullable: false),
                        UpdatedAt = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Mentees", t => t.MenteeId, cascadeDelete: true)
                .Index(t => t.MenteeId);
            
            AddColumn("dbo.Users", "EmailConfirmed", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Facilitators", "UserId", "dbo.Users");
            DropForeignKey("dbo.Mentees", "UserId", "dbo.Users");
            DropForeignKey("dbo.Schedules", "MenteeId", "dbo.Mentees");
            DropForeignKey("dbo.Mentees", "FacilitatorId", "dbo.Facilitators");
            DropForeignKey("dbo.Comments", "UserId", "dbo.Users");
            DropForeignKey("dbo.Comments", "ForumId", "dbo.Fora");
            DropIndex("dbo.Schedules", new[] { "MenteeId" });
            DropIndex("dbo.Mentees", new[] { "FacilitatorId" });
            DropIndex("dbo.Mentees", new[] { "UserId" });
            DropIndex("dbo.Facilitators", new[] { "UserId" });
            DropIndex("dbo.Comments", new[] { "ForumId" });
            DropIndex("dbo.Comments", new[] { "UserId" });
            DropColumn("dbo.Users", "EmailConfirmed");
            DropTable("dbo.Schedules");
            DropTable("dbo.Mentees");
            DropTable("dbo.Facilitators");
            DropTable("dbo.Fora");
            DropTable("dbo.Comments");
        }
    }
}
