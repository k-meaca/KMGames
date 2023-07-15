namespace KMGames.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveRelationBetweenUsersAndSales : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Sales", "Customer_UserId", "dbo.Users");
            DropIndex("dbo.Sales", new[] { "Customer_UserId" });
            DropColumn("dbo.Sales", "Customer_UserId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Sales", "Customer_UserId", c => c.Int());
            CreateIndex("dbo.Sales", "Customer_UserId");
            AddForeignKey("dbo.Sales", "Customer_UserId", "dbo.Users", "UserId");
        }
    }
}
