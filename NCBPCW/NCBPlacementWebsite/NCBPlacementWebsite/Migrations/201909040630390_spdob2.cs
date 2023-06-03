namespace NCBPlacementWebsite.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class spdob2 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.StudentProfiles", "DOB", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.StudentProfiles", "DOB", c => c.String(nullable: false));
        }
    }
}
