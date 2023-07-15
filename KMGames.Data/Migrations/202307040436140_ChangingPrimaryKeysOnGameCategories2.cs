namespace KMGames.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangingPrimaryKeysOnGameCategories2 : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.GameCategories");
            AddPrimaryKey("dbo.GameCategories", new[] { "GameId", "CategoryId" });
            DropForeignKey("dbo.GameCategories", "GameId");
            DropForeignKey("dbo.GameCategories", "Category_CategoryId");
        }
        
        public override void Down()
        {
            DropPrimaryKey("dbo.GameCategories");
            AddPrimaryKey("dbo.GameCategories", "CategoryId");
            AddForeignKey("dbo.GameCategories", "GameId", "dbo.Games");
            AddForeignKey("dbo.GameCategories", "Category_CategoryId", "dbo.Categories");
        }
    }
}
