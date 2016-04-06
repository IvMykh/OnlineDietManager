namespace OnlineDietManager.Domain.Practice.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Mig_5 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Meals",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Description = c.String(),
                        Time = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            AddColumn("dbo.Dishes", "Meal_ID", c => c.Int());
            CreateIndex("dbo.Dishes", "Meal_ID");
            AddForeignKey("dbo.Dishes", "Meal_ID", "dbo.Meals", "ID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Dishes", "Meal_ID", "dbo.Meals");
            DropIndex("dbo.Dishes", new[] { "Meal_ID" });
            DropColumn("dbo.Dishes", "Meal_ID");
            DropTable("dbo.Meals");
        }
    }
}
