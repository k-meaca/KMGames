namespace KMGames.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ModifyUserProperty : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Sales", "UserId", "dbo.Users");
            DropIndex("dbo.Users", new[] { "CountryId" });
            DropIndex("dbo.Sales", new[] { "UserId" });
            RenameColumn(table: "dbo.Users", name: "CountryId", newName: "Country_CountryId");
            AlterColumn("dbo.Users", "Country_CountryId", c => c.Int());
            CreateIndex("dbo.Users", "Country_CountryId");
            DropColumn("dbo.Users", "NickName");
            DropColumn("dbo.Users", "Email");
            DropColumn("dbo.Users", "CreationDate");
            DropColumn("dbo.Users", "Address");
            DropColumn("dbo.Users", "RowVersion");
            DropColumn("dbo.Sales", "UserId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Sales", "UserId", c => c.Int(nullable: false));
            AddColumn("dbo.Users", "RowVersion", c => c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"));
            AddColumn("dbo.Users", "Address", c => c.String());
            AddColumn("dbo.Users", "CreationDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.Users", "Email", c => c.String());
            AddColumn("dbo.Users", "NickName", c => c.String());
            DropIndex("dbo.Users", new[] { "Country_CountryId" });
            AlterColumn("dbo.Users", "Country_CountryId", c => c.Int(nullable: false));
            RenameColumn(table: "dbo.Users", name: "Country_CountryId", newName: "CountryId");
            CreateIndex("dbo.Sales", "UserId");
            CreateIndex("dbo.Users", "CountryId");
            AddForeignKey("dbo.Sales", "UserId", "dbo.Users", "UserId");
        }
    }
}
