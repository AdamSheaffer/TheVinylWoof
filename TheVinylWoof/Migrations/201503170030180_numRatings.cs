namespace VinylWoof.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class numRatings : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Albums",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        Artist = c.String(),
                        Genre = c.String(),
                        Description = c.String(),
                        UserId = c.Int(nullable: false),
                        BuyerId = c.Int(nullable: false),
                        Cost = c.Int(nullable: false),
                        IsSold = c.Boolean(nullable: false),
                        CoverLocation = c.String(),
                        Condition = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Username = c.String(),
                        Credits = c.Int(nullable: false),
                        Rating = c.Decimal(nullable: false, precision: 18, scale: 2),
                        NumRatings = c.Int(nullable: false),
                        Bio = c.String(),
                        StreetAddress = c.String(),
                        City = c.String(),
                        State = c.String(),
                        Zip = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Albums", "UserId", "dbo.Users");
            DropIndex("dbo.Albums", new[] { "UserId" });
            DropTable("dbo.Users");
            DropTable("dbo.Albums");
        }
    }
}
