namespace KMGames.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateDevelopersTableAndItsRelationshipWithGames : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Developers",
                c => new
                    {
                        DeveloperId = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 100),
                        Address = c.String(maxLength: 30),
                        RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                    })
                .PrimaryKey(t => t.DeveloperId)
                .Index(t => t.Name, unique: true, name: "IX_Developers_Name");
            
            AddColumn("dbo.Games", "Description", c => c.String(maxLength: 250));
            AddColumn("dbo.Games", "Release", c => c.DateTime(nullable: false));
            AddColumn("dbo.Games", "DeveloperId", c => c.Int(nullable: false));
            CreateIndex("dbo.Games", "DeveloperId");
            AddForeignKey("dbo.Games", "DeveloperId", "dbo.Developers", "DeveloperId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Games", "DeveloperId", "dbo.Developers");
            DropIndex("dbo.Developers", "IX_Developers_Name");
            DropIndex("dbo.Games", new[] { "DeveloperId" });
            DropColumn("dbo.Games", "DeveloperId");
            DropColumn("dbo.Games", "Release");
            DropColumn("dbo.Games", "Description");
            DropTable("dbo.Developers");
        }
    }
}
