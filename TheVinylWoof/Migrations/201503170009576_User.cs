namespace TheVinylWoof.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class User : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Albums", "UserId", c => c.Int(nullable: false));
            DropColumn("dbo.Albums", "ApplicationUserId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Albums", "ApplicationUserId", c => c.Int(nullable: false));
            DropColumn("dbo.Albums", "UserId");
        }
    }
}
