namespace NCBPlacementWebsite.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class sprofiles : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.StudentProfiles");
            AddColumn("dbo.StudentProfiles", "State", c => c.String());
            DropColumn("dbo.StudentProfiles", "Id");
            AddColumn("dbo.StudentProfiles", "Id", c => c.Guid(nullable: false, identity: true));
            AddPrimaryKey("dbo.StudentProfiles", "Id");
        }
        
        public override void Down()
        {
            DropPrimaryKey("dbo.StudentProfiles");
            AlterColumn("dbo.StudentProfiles", "Id", c => c.Int(nullable: false, identity: true));
            DropColumn("dbo.StudentProfiles", "State");
            AddPrimaryKey("dbo.StudentProfiles", "Id");
        }
    }
}
