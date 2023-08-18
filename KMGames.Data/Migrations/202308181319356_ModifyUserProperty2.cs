namespace KMGames.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ModifyUserProperty2 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Users", "Country_CountryId", "dbo.Countries");
            DropIndex("dbo.Users", new[] { "Country_CountryId" });
            DropTable("dbo.Users");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        UserId = c.Int(nullable: false, identity: true),
                        Country_CountryId = c.Int(),
                    })
                .PrimaryKey(t => t.UserId);
            
            CreateIndex("dbo.Users", "Country_CountryId");
            AddForeignKey("dbo.Users", "Country_CountryId", "dbo.Countries", "CountryId");
        }
    }
}
