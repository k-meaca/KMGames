namespace KMGames.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangingNameOfIdAttributeCategories2 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        CategoryId = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50),
                        RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                    })
                .PrimaryKey(t => t.CategoryId)
                .Index(t => t.Name, unique: true, name: "IX_Categories_Name");
            
            CreateTable(
                "dbo.GameCategories",
                c => new
                    {
                        CategoryId = c.Int(nullable: false),
                        GameId = c.Int(nullable: false),
                        RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                    })
                .PrimaryKey(t => new { t.CategoryId, t.GameId })
                .ForeignKey("dbo.Games", t => t.GameId)
                .ForeignKey("dbo.Categories", t => t.CategoryId)
                .Index(t => t.CategoryId)
                .Index(t => t.GameId);
            
            CreateTable(
                "dbo.Games",
                c => new
                    {
                        GameId = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false, maxLength: 100),
                        ActualPrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Description = c.String(maxLength: 250),
                        Release = c.DateTime(nullable: false),
                        RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                        DeveloperId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.GameId)
                .ForeignKey("dbo.Developers", t => t.DeveloperId)
                .Index(t => t.Title, unique: true, name: "IX_Games_Title")
                .Index(t => t.DeveloperId);
            
            CreateTable(
                "dbo.Developers",
                c => new
                    {
                        DeveloperId = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 100),
                        Address = c.String(maxLength: 30),
                        RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                        CountryId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.DeveloperId)
                .ForeignKey("dbo.Countries", t => t.CountryId)
                .Index(t => t.Name, unique: true, name: "IX_Developers_Name")
                .Index(t => t.CountryId);
            
            CreateTable(
                "dbo.PlayersGames",
                c => new
                    {
                        GameId = c.Int(nullable: false),
                        PlayerTypeId = c.Int(nullable: false),
                        RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                    })
                .PrimaryKey(t => new { t.GameId, t.PlayerTypeId })
                .ForeignKey("dbo.PlayerTypes", t => t.PlayerTypeId)
                .ForeignKey("dbo.Games", t => t.GameId)
                .Index(t => t.GameId)
                .Index(t => t.PlayerTypeId);
            
            CreateTable(
                "dbo.PlayerTypes",
                c => new
                    {
                        PlayerTypeId = c.Int(nullable: false, identity: true),
                        Type = c.String(nullable: false, maxLength: 100),
                        RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                    })
                .PrimaryKey(t => t.PlayerTypeId)
                .Index(t => t.Type, unique: true, name: "IX_PlayerTypes_Type");
            
            CreateTable(
                "dbo.SalesDetails",
                c => new
                    {
                        SaleId = c.Int(nullable: false),
                        GameId = c.Int(nullable: false),
                        SalePrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                        RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                    })
                .PrimaryKey(t => new { t.SaleId, t.GameId })
                .ForeignKey("dbo.Sales", t => t.SaleId)
                .ForeignKey("dbo.Games", t => t.GameId)
                .Index(t => t.SaleId)
                .Index(t => t.GameId);
            
            CreateTable(
                "dbo.Sales",
                c => new
                    {
                        SaleId = c.Int(nullable: false, identity: true),
                        Date = c.DateTime(nullable: false),
                        Total = c.Decimal(precision: 18, scale: 2),
                        RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                        Customer_UserId = c.Int(),
                    })
                .PrimaryKey(t => t.SaleId)
                .ForeignKey("dbo.Users", t => t.Customer_UserId)
                .Index(t => t.Customer_UserId);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        UserId = c.Int(nullable: false, identity: true),
                        NickName = c.String(),
                        Email = c.String(),
                        CreationDate = c.DateTime(nullable: false),
                        CountryId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.UserId)
                .ForeignKey("dbo.Countries", t => t.CountryId)
                .Index(t => t.CountryId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.GameCategories", "CategoryId", "dbo.Categories");
            DropForeignKey("dbo.SalesDetails", "GameId", "dbo.Games");
            DropForeignKey("dbo.SalesDetails", "SaleId", "dbo.Sales");
            DropForeignKey("dbo.Sales", "Customer_UserId", "dbo.Users");
            DropForeignKey("dbo.Users", "CountryId", "dbo.Countries");
            DropForeignKey("dbo.PlayersGames", "GameId", "dbo.Games");
            DropForeignKey("dbo.PlayersGames", "PlayerTypeId", "dbo.PlayerTypes");
            DropForeignKey("dbo.GameCategories", "GameId", "dbo.Games");
            DropForeignKey("dbo.Games", "DeveloperId", "dbo.Developers");
            DropForeignKey("dbo.Developers", "CountryId", "dbo.Countries");
            DropIndex("dbo.Users", new[] { "CountryId" });
            DropIndex("dbo.Sales", new[] { "Customer_UserId" });
            DropIndex("dbo.SalesDetails", new[] { "GameId" });
            DropIndex("dbo.SalesDetails", new[] { "SaleId" });
            DropIndex("dbo.PlayerTypes", "IX_PlayerTypes_Type");
            DropIndex("dbo.PlayersGames", new[] { "PlayerTypeId" });
            DropIndex("dbo.PlayersGames", new[] { "GameId" });
            DropIndex("dbo.Developers", new[] { "CountryId" });
            DropIndex("dbo.Developers", "IX_Developers_Name");
            DropIndex("dbo.Games", new[] { "DeveloperId" });
            DropIndex("dbo.Games", "IX_Games_Title");
            DropIndex("dbo.GameCategories", new[] { "GameId" });
            DropIndex("dbo.GameCategories", new[] { "CategoryId" });
            DropIndex("dbo.Categories", "IX_Categories_Name");
            DropTable("dbo.Users");
            DropTable("dbo.Sales");
            DropTable("dbo.SalesDetails");
            DropTable("dbo.PlayerTypes");
            DropTable("dbo.PlayersGames");
            DropTable("dbo.Developers");
            DropTable("dbo.Games");
            DropTable("dbo.GameCategories");
            DropTable("dbo.Categories");
        }
    }
}
