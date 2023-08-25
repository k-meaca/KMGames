namespace KMGames.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateCartsTableWithRelationshipWithGames : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Carts",
                c => new
                    {
                        CartId = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 168),
                        RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                    })
                .PrimaryKey(t => t.CartId)
                .ForeignKey("dbo.Users", t => t.UserId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.GamesInCarts",
                c => new
                    {
                        CartId = c.Int(nullable: false),
                        GameId = c.Int(nullable: false),
                        RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                    })
                .PrimaryKey(t => new { t.CartId, t.GameId })
                .ForeignKey("dbo.Games", t => t.GameId)
                .ForeignKey("dbo.Carts", t => t.CartId)
                .Index(t => t.CartId)
                .Index(t => t.GameId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Carts", "UserId", "dbo.Users");
            DropForeignKey("dbo.GamesInCarts", "CartId", "dbo.Carts");
            DropForeignKey("dbo.GamesInCarts", "GameId", "dbo.Games");
            DropIndex("dbo.GamesInCarts", new[] { "GameId" });
            DropIndex("dbo.GamesInCarts", new[] { "CartId" });
            DropIndex("dbo.Carts", new[] { "UserId" });
            DropTable("dbo.GamesInCarts");
            DropTable("dbo.Carts");
        }
    }
}
