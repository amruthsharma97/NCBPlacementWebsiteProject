namespace NCBPlacementWebsite.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class sprof : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.StudentProfiles", "Aadnum", c => c.String(maxLength: 12));
            AlterColumn("dbo.StudentProfiles", "URNo", c => c.String(maxLength: 10));
            AlterColumn("dbo.StudentProfiles", "FName", c => c.String(nullable: false, maxLength: 20));
            AlterColumn("dbo.StudentProfiles", "MName", c => c.String(maxLength: 20));
            AlterColumn("dbo.StudentProfiles", "LName", c => c.String(nullable: false, maxLength: 20));
            AlterColumn("dbo.StudentProfiles", "MNumber", c => c.String(nullable: false, maxLength: 10));
            AlterColumn("dbo.StudentProfiles", "PostalCode", c => c.String(maxLength: 6));
            AlterColumn("dbo.StudentProfiles", "City", c => c.String(maxLength: 50));
            AlterColumn("dbo.StudentProfiles", "State", c => c.String(maxLength: 50));
            AlterColumn("dbo.StudentProfiles", "Country", c => c.String(maxLength: 50));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.StudentProfiles", "Country", c => c.String());
            AlterColumn("dbo.StudentProfiles", "State", c => c.String());
            AlterColumn("dbo.StudentProfiles", "City", c => c.String());
            AlterColumn("dbo.StudentProfiles", "PostalCode", c => c.Int(nullable: false));
            AlterColumn("dbo.StudentProfiles", "MNumber", c => c.String(nullable: false));
            AlterColumn("dbo.StudentProfiles", "LName", c => c.String(nullable: false));
            AlterColumn("dbo.StudentProfiles", "MName", c => c.String());
            AlterColumn("dbo.StudentProfiles", "FName", c => c.String(nullable: false));
            AlterColumn("dbo.StudentProfiles", "URNo", c => c.String());
            AlterColumn("dbo.StudentProfiles", "Aadnum", c => c.Long(nullable: false));
        }
    }
}
