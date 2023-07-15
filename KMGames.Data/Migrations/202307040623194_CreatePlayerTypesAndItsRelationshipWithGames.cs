namespace KMGames.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreatePlayerTypesAndItsRelationshipWithGames : DbMigration
    {
        public override void Up()
        {
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
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PlayersGames", "GameId", "dbo.Games");
            DropForeignKey("dbo.PlayersGames", "PlayerTypeId", "dbo.PlayerTypes");
            DropIndex("dbo.PlayerTypes", "IX_PlayerTypes_Type");
            DropIndex("dbo.PlayersGames", new[] { "PlayerTypeId" });
            DropIndex("dbo.PlayersGames", new[] { "GameId" });
            DropTable("dbo.PlayerTypes");
            DropTable("dbo.PlayersGames");
        }
    }
}
