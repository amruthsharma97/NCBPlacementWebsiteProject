namespace NCBPlacementWebsite.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class contlist : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ContactLists",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50),
                        Dept = c.String(nullable: false, maxLength: 100),
                        PhoneNo = c.String(nullable: false, maxLength: 10),
                        MailId = c.String(nullable: false),
                        ImgPath = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.ContactLists");
        }
    }
}
