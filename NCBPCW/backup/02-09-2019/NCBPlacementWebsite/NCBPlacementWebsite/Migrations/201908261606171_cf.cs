namespace NCBPlacementWebsite.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class cf : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CurriculumFeedbacks",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        SName = c.String(nullable: false),
                        URNo = c.String(nullable: false, maxLength: 10),
                        Branch = c.String(nullable: false),
                        Subject = c.String(nullable: false),
                        SYear = c.Int(nullable: false),
                        EYear = c.Int(nullable: false),
                        Q1 = c.String(nullable: false),
                        Q2 = c.String(nullable: false),
                        Q3 = c.String(nullable: false),
                        Q4 = c.String(nullable: false),
                        Q5 = c.String(nullable: false),
                        Q6 = c.String(nullable: false),
                        Q7 = c.String(nullable: false),
                        Q8 = c.String(nullable: false),
                        Q9 = c.String(nullable: false),
                        Q10 = c.String(nullable: false),
                        Q11 = c.String(nullable: false),
                        Q12 = c.String(nullable: false),
                        Q13 = c.String(nullable: false),
                        Q14 = c.String(nullable: false),
                        Q15 = c.String(nullable: false),
                        Q16 = c.String(nullable: false),
                        Q17 = c.String(nullable: false),
                        Q18 = c.String(nullable: false),
                        Q19 = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.CurriculumFeedbacks");
        }
    }
}
