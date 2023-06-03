namespace NCBPlacementWebsite.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class sprof3 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.StudentProfiles", "Aadnum", c => c.String(nullable: false, maxLength: 12));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.StudentProfiles", "Aadnum", c => c.String(maxLength: 12));
        }
    }
}
