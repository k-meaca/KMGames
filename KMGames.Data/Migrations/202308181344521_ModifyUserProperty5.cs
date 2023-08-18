namespace KMGames.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ModifyUserProperty5 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "CityId", c => c.Int(nullable: false));
            CreateIndex("dbo.Users", "CityId");
            AddForeignKey("dbo.Users", "CityId", "dbo.Cities", "CityId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Users", "CityId", "dbo.Cities");
            DropIndex("dbo.Users", new[] { "CityId" });
            DropColumn("dbo.Users", "CityId");
        }
    }
}
