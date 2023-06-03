namespace NCBPlacementWebsite.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class contlist1 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.ContactLists", "ImgPath");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ContactLists", "ImgPath", c => c.String());
        }
    }
}
