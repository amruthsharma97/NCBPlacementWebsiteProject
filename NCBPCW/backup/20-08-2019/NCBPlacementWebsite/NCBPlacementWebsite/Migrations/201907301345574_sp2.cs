namespace NCBPlacementWebsite.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class sp2 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.StudentProfiles", "Status", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.StudentProfiles", "Status", c => c.String());
        }
    }
}
