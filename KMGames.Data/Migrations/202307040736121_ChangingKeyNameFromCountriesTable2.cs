namespace KMGames.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangingKeyNameFromCountriesTable2 : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Countries", "IX_Countries_Name");
            DropTable("dbo.Countries");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Countries",
                c => new
                    {
                        IdCountry = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.IdCountry);
            
            CreateIndex("dbo.Countries", "Name", unique: true, name: "IX_Countries_Name");
        }
    }
}
