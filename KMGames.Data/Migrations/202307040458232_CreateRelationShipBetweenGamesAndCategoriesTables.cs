namespace KMGames.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateRelationShipBetweenGamesAndCategoriesTables : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.GameCategories",
                c => new
                    {
                        CategoryId = c.Int(nullable: false),
                        GameId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.CategoryId, t.GameId })
                .ForeignKey("dbo.Games", t => t.GameId)
                .ForeignKey("dbo.Categories", t => t.CategoryId)
                .Index(t => t.CategoryId)
                .Index(t => t.GameId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.GameCategories", "CategoryId", "dbo.Categories");
            DropForeignKey("dbo.GameCategories", "GameId", "dbo.Games");
            DropIndex("dbo.GameCategories", new[] { "GameId" });
            DropIndex("dbo.GameCategories", new[] { "CategoryId" });
            DropTable("dbo.GameCategories");
        }
    }
}
