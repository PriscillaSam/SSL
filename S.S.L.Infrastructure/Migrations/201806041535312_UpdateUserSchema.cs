namespace S.S.L.Infrastructure.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateUserSchema : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "EmploymentStatus", c => c.String());
            AddColumn("dbo.Users", "Country", c => c.String());
            AddColumn("dbo.Users", "State", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Users", "State");
            DropColumn("dbo.Users", "Country");
            DropColumn("dbo.Users", "EmploymentStatus");
        }
    }
}
