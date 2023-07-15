namespace KMGames.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateRowVersionAtributeForCountries : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Countries", "RowVersion", c => c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Countries", "RowVersion");
        }
    }
}
