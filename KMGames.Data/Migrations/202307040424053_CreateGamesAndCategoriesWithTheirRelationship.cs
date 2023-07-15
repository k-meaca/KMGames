namespace KMGames.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateGamesAndCategoriesWithTheirRelationship : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        CategoryId = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50),
                        RowVersion = c.Binary(),
                    })
                .PrimaryKey(t => t.CategoryId)
                .Index(t => t.Name, unique: true, name: "IX_Categories_Name");
            
            CreateTable(
                "dbo.GameCategories",
                c => new
                    {
                        CategoryId = c.String(nullable: false, maxLength: 128),
                        GameId = c.Int(nullable: false),
                        Category_CategoryId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.CategoryId)
                .ForeignKey("dbo.Games", t => t.GameId)
                .ForeignKey("dbo.Categories", t => t.Category_CategoryId)
                .Index(t => t.GameId)
                .Index(t => t.Category_CategoryId);
            
            CreateTable(
                "dbo.Games",
                c => new
                    {
                        GameId = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false, maxLength: 100),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        RowVersion = c.Binary(),
                    })
                .PrimaryKey(t => t.GameId)
                .Index(t => t.Title, unique: true, name: "IX_Games_Title");
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.GameCategories", "Category_CategoryId", "dbo.Categories");
            DropForeignKey("dbo.GameCategories", "GameId", "dbo.Games");
            DropIndex("dbo.Games", "IX_Games_Title");
            DropIndex("dbo.GameCategories", new[] { "Category_CategoryId" });
            DropIndex("dbo.GameCategories", new[] { "GameId" });
            DropIndex("dbo.Categories", "IX_Categories_Name");
            DropTable("dbo.Games");
            DropTable("dbo.GameCategories");
            DropTable("dbo.Categories");
        }
    }
}
