namespace KMGames.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangingNameOfIdAttributeCategories : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Developers", "CountryId", "dbo.Countries");
            DropForeignKey("dbo.Games", "DeveloperId", "dbo.Developers");
            DropForeignKey("dbo.GameCategories", "GameId", "dbo.Games");
            DropForeignKey("dbo.PlayersGames", "PlayerTypeId", "dbo.PlayerTypes");
            DropForeignKey("dbo.PlayersGames", "GameId", "dbo.Games");
            DropForeignKey("dbo.Users", "CountryId", "dbo.Countries");
            DropForeignKey("dbo.Sales", "Customer_UserId", "dbo.Users");
            DropForeignKey("dbo.SalesDetails", "SaleId", "dbo.Sales");
            DropForeignKey("dbo.SalesDetails", "GameId", "dbo.Games");
            DropForeignKey("dbo.GameCategories", "IdCategory", "dbo.Categories");
            DropIndex("dbo.Categories", "IX_Categories_Name");
            DropIndex("dbo.GameCategories", new[] { "IdCategory" });
            DropIndex("dbo.GameCategories", new[] { "GameId" });
            DropIndex("dbo.Games", "IX_Games_Title");
            DropIndex("dbo.Games", new[] { "DeveloperId" });
            DropIndex("dbo.Developers", "IX_Developers_Name");
            DropIndex("dbo.Developers", new[] { "CountryId" });
            DropIndex("dbo.PlayersGames", new[] { "GameId" });
            DropIndex("dbo.PlayersGames", new[] { "PlayerTypeId" });
            DropIndex("dbo.PlayerTypes", "IX_PlayerTypes_Type");
            DropIndex("dbo.SalesDetails", new[] { "SaleId" });
            DropIndex("dbo.SalesDetails", new[] { "GameId" });
            DropIndex("dbo.Sales", new[] { "Customer_UserId" });
            DropIndex("dbo.Users", "IX_Users_NickName");
            DropIndex("dbo.Users", new[] { "CountryId" });
            DropTable("dbo.Categories");
            DropTable("dbo.GameCategories");
            DropTable("dbo.Games");
            DropTable("dbo.Developers");
            DropTable("dbo.PlayersGames");
            DropTable("dbo.PlayerTypes");
            DropTable("dbo.SalesDetails");
            DropTable("dbo.Sales");
            DropTable("dbo.Users");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        UserId = c.Int(nullable: false, identity: true),
                        NickName = c.String(nullable: false, maxLength: 30),
                        Email = c.String(nullable: false),
                        CreationDate = c.DateTime(nullable: false),
                        CountryId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.UserId);
            
            CreateTable(
                "dbo.Sales",
                c => new
                    {
                        SaleId = c.Int(nullable: false, identity: true),
                        Date = c.DateTime(nullable: false),
                        IdUser = c.Int(nullable: false),
                        Total = c.Decimal(precision: 18, scale: 2),
                        RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                        Customer_UserId = c.Int(),
                    })
                .PrimaryKey(t => t.SaleId);
            
            CreateTable(
                "dbo.SalesDetails",
                c => new
                    {
                        SaleId = c.Int(nullable: false),
                        GameId = c.Int(nullable: false),
                        SalePrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                        RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                    })
                .PrimaryKey(t => new { t.SaleId, t.GameId });
            
            CreateTable(
                "dbo.PlayerTypes",
                c => new
                    {
                        PlayerTypeId = c.Int(nullable: false, identity: true),
                        Type = c.String(nullable: false, maxLength: 100),
                        RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                    })
                .PrimaryKey(t => t.PlayerTypeId);
            
            CreateTable(
                "dbo.PlayersGames",
                c => new
                    {
                        GameId = c.Int(nullable: false),
                        PlayerTypeId = c.Int(nullable: false),
                        RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                    })
                .PrimaryKey(t => new { t.GameId, t.PlayerTypeId });
            
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
                .PrimaryKey(t => t.DeveloperId);
            
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
                .PrimaryKey(t => t.GameId);
            
            CreateTable(
                "dbo.GameCategories",
                c => new
                    {
                        IdCategory = c.Int(nullable: false),
                        GameId = c.Int(nullable: false),
                        RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                    })
                .PrimaryKey(t => new { t.IdCategory, t.GameId });
            
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        IdCategory = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50),
                        RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                    })
                .PrimaryKey(t => t.IdCategory);
            
            CreateIndex("dbo.Users", "CountryId");
            CreateIndex("dbo.Users", "NickName", unique: true, name: "IX_Users_NickName");
            CreateIndex("dbo.Sales", "Customer_UserId");
            CreateIndex("dbo.SalesDetails", "GameId");
            CreateIndex("dbo.SalesDetails", "SaleId");
            CreateIndex("dbo.PlayerTypes", "Type", unique: true, name: "IX_PlayerTypes_Type");
            CreateIndex("dbo.PlayersGames", "PlayerTypeId");
            CreateIndex("dbo.PlayersGames", "GameId");
            CreateIndex("dbo.Developers", "CountryId");
            CreateIndex("dbo.Developers", "Name", unique: true, name: "IX_Developers_Name");
            CreateIndex("dbo.Games", "DeveloperId");
            CreateIndex("dbo.Games", "Title", unique: true, name: "IX_Games_Title");
            CreateIndex("dbo.GameCategories", "GameId");
            CreateIndex("dbo.GameCategories", "IdCategory");
            CreateIndex("dbo.Categories", "Name", unique: true, name: "IX_Categories_Name");
            AddForeignKey("dbo.GameCategories", "IdCategory", "dbo.Categories", "IdCategory");
            AddForeignKey("dbo.SalesDetails", "GameId", "dbo.Games", "GameId");
            AddForeignKey("dbo.SalesDetails", "SaleId", "dbo.Sales", "SaleId");
            AddForeignKey("dbo.Sales", "Customer_UserId", "dbo.Users", "UserId");
            AddForeignKey("dbo.Users", "CountryId", "dbo.Countries", "CountryId");
            AddForeignKey("dbo.PlayersGames", "GameId", "dbo.Games", "GameId");
            AddForeignKey("dbo.PlayersGames", "PlayerTypeId", "dbo.PlayerTypes", "PlayerTypeId");
            AddForeignKey("dbo.GameCategories", "GameId", "dbo.Games", "GameId");
            AddForeignKey("dbo.Games", "DeveloperId", "dbo.Developers", "DeveloperId");
            AddForeignKey("dbo.Developers", "CountryId", "dbo.Countries", "CountryId");
        }
    }
}
