namespace KMGames.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveAddressAttributeFromDevelopers : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Developers", "Address");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Developers", "Address", c => c.String(maxLength: 30));
        }
    }
}
