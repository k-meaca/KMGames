namespace KMGames.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateImageAttributeForGame : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Games", "Image", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Games", "Image");
        }
    }
}
