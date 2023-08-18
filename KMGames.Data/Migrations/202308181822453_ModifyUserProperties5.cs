namespace KMGames.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ModifyUserProperties5 : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Users", new[] { "CityId" });
            AlterColumn("dbo.Users", "CityId", c => c.Int());
            CreateIndex("dbo.Users", "CityId");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Users", new[] { "CityId" });
            AlterColumn("dbo.Users", "CityId", c => c.Int(nullable: false));
            CreateIndex("dbo.Users", "CityId");
        }
    }
}
