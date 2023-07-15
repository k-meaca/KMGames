namespace KMGames.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateUsersTable : DbMigration
    {
        public override void Up()
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
                        Country_IdCountry = c.Int(),
                    })
                .PrimaryKey(t => t.UserId)
                .ForeignKey("dbo.Countries", t => t.Country_IdCountry)
                .Index(t => t.NickName, unique: true, name: "IX_Users_NickName")
                .Index(t => t.Country_IdCountry);
            
            AddColumn("dbo.Sales", "IdUser", c => c.Int(nullable: false));
            AddColumn("dbo.Sales", "Customer_UserId", c => c.Int());
            CreateIndex("dbo.Sales", "Customer_UserId");
            AddForeignKey("dbo.Sales", "Customer_UserId", "dbo.Users", "UserId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Sales", "Customer_UserId", "dbo.Users");
            DropForeignKey("dbo.Users", "Country_IdCountry", "dbo.Countries");
            DropIndex("dbo.Users", new[] { "Country_IdCountry" });
            DropIndex("dbo.Users", "IX_Users_NickName");
            DropIndex("dbo.Sales", new[] { "Customer_UserId" });
            DropColumn("dbo.Sales", "Customer_UserId");
            DropColumn("dbo.Sales", "IdUser");
            DropTable("dbo.Users");
        }
    }
}
