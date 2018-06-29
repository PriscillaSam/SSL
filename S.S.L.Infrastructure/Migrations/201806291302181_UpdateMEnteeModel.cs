namespace S.S.L.Infrastructure.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateMEnteeModel : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Mentees", "FacilitatorId", "dbo.Facilitators");
            DropIndex("dbo.Mentees", new[] { "FacilitatorId" });
            AlterColumn("dbo.Mentees", "FacilitatorId", c => c.Int());
            CreateIndex("dbo.Mentees", "FacilitatorId");
            AddForeignKey("dbo.Mentees", "FacilitatorId", "dbo.Facilitators", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Mentees", "FacilitatorId", "dbo.Facilitators");
            DropIndex("dbo.Mentees", new[] { "FacilitatorId" });
            AlterColumn("dbo.Mentees", "FacilitatorId", c => c.Int(nullable: false));
            CreateIndex("dbo.Mentees", "FacilitatorId");
            AddForeignKey("dbo.Mentees", "FacilitatorId", "dbo.Facilitators", "Id", cascadeDelete: true);
        }
    }
}
