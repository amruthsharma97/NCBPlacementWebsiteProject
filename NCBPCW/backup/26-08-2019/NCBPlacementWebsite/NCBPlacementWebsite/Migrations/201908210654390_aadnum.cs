namespace NCBPlacementWebsite.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class aadnum : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.StudentProfiles", "Aadnum", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.StudentProfiles", "Aadnum");
        }
    }
}
