namespace KMGames.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateCountriesTableAndItsRelationshipWithDevelopers : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Countries",
                c => new
                    {
                        IdCountry = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.IdCountry)
                .Index(t => t.Name, unique: true, name: "IX_Countries_Name");
            
            AddColumn("dbo.Developers", "IdCountry", c => c.Int(nullable: false));
            CreateIndex("dbo.Developers", "IdCountry");
            AddForeignKey("dbo.Developers", "IdCountry", "dbo.Countries", "IdCountry");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Developers", "IdCountry", "dbo.Countries");
            DropIndex("dbo.Countries", "IX_Countries_Name");
            DropIndex("dbo.Developers", new[] { "IdCountry" });
            DropColumn("dbo.Developers", "IdCountry");
            DropTable("dbo.Countries");
        }
    }
}
