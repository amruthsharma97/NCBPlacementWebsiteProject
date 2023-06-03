namespace NCBPlacementWebsite.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class aadnum1 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.StudentProfiles", "Aadnum", c => c.Long(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.StudentProfiles", "Aadnum", c => c.Int(nullable: false));
        }
    }
}
