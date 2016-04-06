namespace OnlineDietManager.Domain.Practice.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Mig_12 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Courses", "OwnerID", c => c.Int());
            AlterColumn("dbo.Dishes", "OwnerID", c => c.Int());
            AlterColumn("dbo.Ingredients", "OwnerID", c => c.Int());
            CreateIndex("dbo.Courses", "OwnerID");
            CreateIndex("dbo.Dishes", "OwnerID");
            CreateIndex("dbo.Ingredients", "OwnerID");
            AddForeignKey("dbo.Ingredients", "OwnerID", "dbo.Users", "ID");
            AddForeignKey("dbo.Dishes", "OwnerID", "dbo.Users", "ID");
            AddForeignKey("dbo.Courses", "OwnerID", "dbo.Users", "ID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Courses", "OwnerID", "dbo.Users");
            DropForeignKey("dbo.Dishes", "OwnerID", "dbo.Users");
            DropForeignKey("dbo.Ingredients", "OwnerID", "dbo.Users");
            DropIndex("dbo.Ingredients", new[] { "OwnerID" });
            DropIndex("dbo.Dishes", new[] { "OwnerID" });
            DropIndex("dbo.Courses", new[] { "OwnerID" });
            AlterColumn("dbo.Ingredients", "OwnerID", c => c.Int(nullable: false));
            AlterColumn("dbo.Dishes", "OwnerID", c => c.Int(nullable: false));
            AlterColumn("dbo.Courses", "OwnerID", c => c.Int(nullable: false));
        }
    }
}
