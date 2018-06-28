namespace S.S.L.Infrastructure.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateUserRoleTable : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.UserRoles");
            AddColumn("dbo.Comments", "Message", c => c.String());
            AddPrimaryKey("dbo.UserRoles", new[] { "RoleId", "UserId" });
            DropColumn("dbo.Comments", "CommentPost");
            DropColumn("dbo.UserRoles", "Id");
            DropColumn("dbo.UserRoles", "CreatedAt");
            DropColumn("dbo.UserRoles", "UpdatedAt");
        }
        
        public override void Down()
        {
            AddColumn("dbo.UserRoles", "UpdatedAt", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
            AddColumn("dbo.UserRoles", "CreatedAt", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
            AddColumn("dbo.UserRoles", "Id", c => c.Int(nullable: false, identity: true));
            AddColumn("dbo.Comments", "CommentPost", c => c.String());
            DropPrimaryKey("dbo.UserRoles");
            DropColumn("dbo.Comments", "Message");
            AddPrimaryKey("dbo.UserRoles", "Id");
        }
    }
}
