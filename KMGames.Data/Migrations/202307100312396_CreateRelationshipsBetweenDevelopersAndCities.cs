namespace KMGames.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateRelationshipsBetweenDevelopersAndCities : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Developers", "CityId", c => c.Int(nullable: false));
            Sql("UPDATE Developers SET CityId = 1");
            CreateIndex("dbo.Developers", "CityId");
            AddForeignKey("dbo.Developers", "CityId", "dbo.Cities", "CityId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Developers", "CityId", "dbo.Cities");
            DropIndex("dbo.Developers", new[] { "CityId" });
            DropColumn("dbo.Developers", "CityId");
        }
    }
}
