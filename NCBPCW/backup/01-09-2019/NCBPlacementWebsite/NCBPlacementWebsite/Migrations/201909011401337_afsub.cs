namespace NCBPlacementWebsite.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class afsub : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.AppraisalFeedbacks", "Subject");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AppraisalFeedbacks", "Subject", c => c.String(nullable: false));
        }
    }
}
