namespace S.S.L.Infrastructure.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Schemachange : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "PasswordHash", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Users", "PasswordHash");
        }
    }
}
