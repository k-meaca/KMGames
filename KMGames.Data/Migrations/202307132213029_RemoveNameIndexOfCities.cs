namespace KMGames.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveNameIndexOfCities : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Cities", "IX_Cities_Name");
        }
        
        public override void Down()
        {
            CreateIndex("dbo.Cities", "Name", unique: true, name: "IX_Cities_Name");
        }
    }
}
