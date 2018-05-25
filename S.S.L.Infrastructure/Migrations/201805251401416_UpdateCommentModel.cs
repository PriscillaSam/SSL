namespace S.S.L.Infrastructure.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateCommentModel : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Comments", "CommentPost", c => c.String());
            AlterColumn("dbo.Comments", "CreatedAt", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.Comments", "UpdatedAt", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.Fora", "CreatedAt", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.Fora", "UpdatedAt", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.Users", "CreatedAt", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.Users", "UpdatedAt", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.Roles", "CreatedAt", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.Roles", "UpdatedAt", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.Facilitators", "CreatedAt", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.Facilitators", "UpdatedAt", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.Mentees", "CreatedAt", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.Mentees", "UpdatedAt", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.Schedules", "CreatedAt", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.Schedules", "UpdatedAt", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
            DropColumn("dbo.Comments", "Message");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Comments", "Message", c => c.String());
            AlterColumn("dbo.Schedules", "UpdatedAt", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Schedules", "CreatedAt", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Mentees", "UpdatedAt", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Mentees", "CreatedAt", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Facilitators", "UpdatedAt", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Facilitators", "CreatedAt", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Roles", "UpdatedAt", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Roles", "CreatedAt", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Users", "UpdatedAt", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Users", "CreatedAt", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Fora", "UpdatedAt", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Fora", "CreatedAt", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Comments", "UpdatedAt", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Comments", "CreatedAt", c => c.DateTime(nullable: false));
            DropColumn("dbo.Comments", "CommentPost");
        }
    }
}
