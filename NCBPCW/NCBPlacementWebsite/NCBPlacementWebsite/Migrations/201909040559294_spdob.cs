namespace NCBPlacementWebsite.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class spdob : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.StudentProfiles", "DOB", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.StudentProfiles", "DOB", c => c.DateTime(nullable: false));
        }
    }
}
