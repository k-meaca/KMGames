namespace KMGames.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemovingRowVersion : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Categories", "RowVersion");
            DropColumn("dbo.Games", "RowVersion");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Games", "RowVersion", c => c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"));
            AddColumn("dbo.Categories", "RowVersion", c => c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"));
        }
    }
}
