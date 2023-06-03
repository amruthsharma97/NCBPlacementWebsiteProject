namespace NCBPlacementWebsite.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class af2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AppraisalFeedbacks", "LuctName", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.AppraisalFeedbacks", "LuctName");
        }
    }
}
