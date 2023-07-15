namespace KMGames.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangingPrimaryKeysOnGameCategories3 : DbMigration
    {
        public override void Up()
        {
            DropTable("dbo.GameCategories");
        }
        
        public override void Down()
        {
            
        }
    }
}
