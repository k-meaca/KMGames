namespace KMGames.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangingKeyNameFromCountriesTable : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Developers", "IdCountry", "dbo.Countries");
            DropForeignKey("dbo.Users", "Country_IdCountry", "dbo.Countries");
            DropIndex("dbo.Developers", new[] { "IdCountry" });
            DropIndex("dbo.Users", new[] { "Country_IdCountry" });
            DropColumn("dbo.Developers", "IdCountry");
            DropColumn("dbo.Users", "CountryId");
            DropColumn("dbo.Users", "Country_IdCountry");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Users", "Country_IdCountry", c => c.Int());
            AddColumn("dbo.Users", "CountryId", c => c.Int(nullable: false));
            AddColumn("dbo.Developers", "IdCountry", c => c.Int(nullable: false));
            CreateIndex("dbo.Users", "Country_IdCountry");
            CreateIndex("dbo.Developers", "IdCountry");
            AddForeignKey("dbo.Users", "Country_IdCountry", "dbo.Countries", "IdCountry");
            AddForeignKey("dbo.Developers", "IdCountry", "dbo.Countries", "IdCountry");
        }
    }
}
