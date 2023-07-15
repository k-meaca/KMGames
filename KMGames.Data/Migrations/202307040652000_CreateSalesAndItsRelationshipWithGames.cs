namespace KMGames.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateSalesAndItsRelationshipWithGames : DbMigration
    {
        public override void Up()
        {
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
                    })
                .PrimaryKey(t => t.SaleId);
            
            AddColumn("dbo.Games", "ActualPrice", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            DropColumn("dbo.Games", "Price");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Games", "Price", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            DropForeignKey("dbo.SalesDetails", "GameId", "dbo.Games");
            DropForeignKey("dbo.SalesDetails", "SaleId", "dbo.Sales");
            DropIndex("dbo.SalesDetails", new[] { "GameId" });
            DropIndex("dbo.SalesDetails", new[] { "SaleId" });
            DropColumn("dbo.Games", "ActualPrice");
            DropTable("dbo.Sales");
            DropTable("dbo.SalesDetails");
        }
    }
}
