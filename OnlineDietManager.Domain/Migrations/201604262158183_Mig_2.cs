namespace OnlineDietManager.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Mig_2 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Ingredients", "OwnerID", "dbo.Users");
            DropForeignKey("dbo.Dishes", "OwnerID", "dbo.Users");
            DropForeignKey("dbo.Courses", "OwnerID", "dbo.Users");
            DropIndex("dbo.Courses", new[] { "OwnerID" });
            DropIndex("dbo.Dishes", new[] { "OwnerID" });
            DropIndex("dbo.Ingredients", new[] { "OwnerID" });
            DropTable("dbo.Users");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Login = c.String(nullable: false, maxLength: 50),
                        Password = c.String(nullable: false),
                        IsAdmin = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateIndex("dbo.Ingredients", "OwnerID");
            CreateIndex("dbo.Dishes", "OwnerID");
            CreateIndex("dbo.Courses", "OwnerID");
            AddForeignKey("dbo.Courses", "OwnerID", "dbo.Users", "ID");
            AddForeignKey("dbo.Dishes", "OwnerID", "dbo.Users", "ID");
            AddForeignKey("dbo.Ingredients", "OwnerID", "dbo.Users", "ID");
        }
    }
}
