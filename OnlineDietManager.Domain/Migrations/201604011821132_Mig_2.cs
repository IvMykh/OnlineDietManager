namespace OnlineDietManager.Domain.Practice.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Mig_2 : DbMigration
    {
        public override void Up()
        {
            CreateIndex("dbo.DishComponents", "ID");
            AddForeignKey("dbo.DishComponents", "ID", "dbo.Ingredients", "ID", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.DishComponents", "ID", "dbo.Ingredients");
            DropIndex("dbo.DishComponents", new[] { "ID" });
        }
    }
}
