namespace KMGames.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangingKeyNameFromCountriesTable3 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Countries",
                c => new
                    {
                        CountryId = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.CountryId)
                .Index(t => t.Name, unique: true, name: "IX_Countries_Name");
            
            AddColumn("dbo.Developers", "CountryId", c => c.Int(nullable: false));
            AddColumn("dbo.Users", "CountryId", c => c.Int(nullable: false));
            CreateIndex("dbo.Developers", "CountryId");
            CreateIndex("dbo.Users", "CountryId");
            AddForeignKey("dbo.Developers", "CountryId", "dbo.Countries", "CountryId");
            AddForeignKey("dbo.Users", "CountryId", "dbo.Countries", "CountryId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Users", "CountryId", "dbo.Countries");
            DropForeignKey("dbo.Developers", "CountryId", "dbo.Countries");
            DropIndex("dbo.Users", new[] { "CountryId" });
            DropIndex("dbo.Countries", "IX_Countries_Name");
            DropIndex("dbo.Developers", new[] { "CountryId" });
            DropColumn("dbo.Users", "CountryId");
            DropColumn("dbo.Developers", "CountryId");
            DropTable("dbo.Countries");
        }
    }
}
