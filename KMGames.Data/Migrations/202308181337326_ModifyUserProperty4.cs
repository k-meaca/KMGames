namespace KMGames.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ModifyUserProperty4 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Sales", "UserId", c => c.String(maxLength: 168));
            CreateIndex("dbo.Sales", "UserId");
            AddForeignKey("dbo.Sales", "UserId", "dbo.Users", "UserId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Sales", "UserId", "dbo.Users");
            DropIndex("dbo.Sales", new[] { "UserId" });
            DropColumn("dbo.Sales", "UserId");
        }
    }
}
