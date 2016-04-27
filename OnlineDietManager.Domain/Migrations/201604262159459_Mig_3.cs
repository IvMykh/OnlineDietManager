namespace OnlineDietManager.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Mig_3 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Courses", "OwnerID", c => c.String(maxLength: 128));
            AlterColumn("dbo.Dishes", "OwnerID", c => c.String(maxLength: 128));
            AlterColumn("dbo.Ingredients", "OwnerID", c => c.String(maxLength: 128));
            CreateIndex("dbo.Courses", "OwnerID");
            CreateIndex("dbo.Dishes", "OwnerID");
            CreateIndex("dbo.Ingredients", "OwnerID");
            AddForeignKey("dbo.Ingredients", "OwnerID", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.Dishes", "OwnerID", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.Courses", "OwnerID", "dbo.AspNetUsers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Courses", "OwnerID", "dbo.AspNetUsers");
            DropForeignKey("dbo.Dishes", "OwnerID", "dbo.AspNetUsers");
            DropForeignKey("dbo.Ingredients", "OwnerID", "dbo.AspNetUsers");
            DropIndex("dbo.Ingredients", new[] { "OwnerID" });
            DropIndex("dbo.Dishes", new[] { "OwnerID" });
            DropIndex("dbo.Courses", new[] { "OwnerID" });
            AlterColumn("dbo.Ingredients", "OwnerID", c => c.Int());
            AlterColumn("dbo.Dishes", "OwnerID", c => c.Int());
            AlterColumn("dbo.Courses", "OwnerID", c => c.Int());
        }
    }
}
