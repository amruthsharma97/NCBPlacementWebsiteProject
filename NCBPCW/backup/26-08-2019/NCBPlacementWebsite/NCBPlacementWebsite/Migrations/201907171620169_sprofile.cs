namespace NCBPlacementWebsite.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class sprofile : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.StudentProfiles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        URNo = c.String(),
                        FName = c.String(nullable: false),
                        MName = c.String(),
                        LName = c.String(nullable: false),
                        MNumber = c.String(nullable: false),
                        EmailId = c.String(nullable: false),
                        DOB = c.DateTime(nullable: false),
                        AddrLine1 = c.String(),
                        AddrLine2 = c.String(),
                        PostalCode = c.Int(nullable: false),
                        City = c.String(),
                        Country = c.String(),
                        SSLCPercentage = c.Single(nullable: false),
                        SSLCBoard = c.String(),
                        NameOfSchool = c.String(),
                        SSLCPassingState = c.String(),
                        SSLCPassingCOuntry = c.String(),
                        SSLCYOP = c.Int(nullable: false),
                        PUCPercentage = c.Single(nullable: false),
                        PUCBoard = c.String(),
                        NameOfClg = c.String(),
                        PUCPassingState = c.String(),
                        PUCPassingCOuntry = c.String(),
                        PUCYOP = c.Int(nullable: false),
                        DegreePercentage = c.Single(nullable: false),
                        Branch = c.String(nullable: false),
                        DegreeYOP = c.Int(nullable: false),
                        Status = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.StudentProfiles");
        }
    }
}
