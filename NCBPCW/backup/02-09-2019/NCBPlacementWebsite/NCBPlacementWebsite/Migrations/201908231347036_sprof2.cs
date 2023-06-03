namespace NCBPlacementWebsite.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class sprof2 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.StudentProfiles", "SSLCPassingState", c => c.String(maxLength: 50));
            AlterColumn("dbo.StudentProfiles", "SSLCPassingCOuntry", c => c.String(maxLength: 50));
            AlterColumn("dbo.StudentProfiles", "PUCPassingState", c => c.String(maxLength: 50));
            AlterColumn("dbo.StudentProfiles", "PUCPassingCOuntry", c => c.String(maxLength: 50));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.StudentProfiles", "PUCPassingCOuntry", c => c.String());
            AlterColumn("dbo.StudentProfiles", "PUCPassingState", c => c.String());
            AlterColumn("dbo.StudentProfiles", "SSLCPassingCOuntry", c => c.String());
            AlterColumn("dbo.StudentProfiles", "SSLCPassingState", c => c.String());
        }
    }
}
