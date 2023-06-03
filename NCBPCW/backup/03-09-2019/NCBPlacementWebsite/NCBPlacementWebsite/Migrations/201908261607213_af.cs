namespace NCBPlacementWebsite.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class af : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AppraisalFeedbacks",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Branch = c.String(nullable: false),
                        Subject = c.String(nullable: false),
                        SYear = c.Int(nullable: false),
                        EYear = c.Int(nullable: false),
                        Q1 = c.Int(nullable: false),
                        Q2 = c.Int(nullable: false),
                        Q3 = c.Int(nullable: false),
                        Q4 = c.Int(nullable: false),
                        Q5 = c.Int(nullable: false),
                        Q6 = c.Int(nullable: false),
                        Q7 = c.Int(nullable: false),
                        Q8 = c.Int(nullable: false),
                        Q9 = c.Int(nullable: false),
                        Q10 = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.AppraisalFeedbacks");
        }
    }
}
