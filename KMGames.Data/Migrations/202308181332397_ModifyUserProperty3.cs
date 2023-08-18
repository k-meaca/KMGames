namespace KMGames.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ModifyUserProperty3 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 168),
                        FirstName = c.String(maxLength: 256),
                        LastName = c.String(maxLength: 256),
                        Email = c.String(maxLength: 256),
                        Address = c.String(maxLength: 256),
                        DNI = c.String(maxLength: 25),
                        DateOfBirth = c.DateTime(nullable: false),
                        CreationDate = c.DateTime(nullable: false),
                        RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                    })
                .PrimaryKey(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Users");
        }
    }
}
