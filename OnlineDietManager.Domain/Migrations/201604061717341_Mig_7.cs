namespace OnlineDietManager.Domain.Practice.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Mig_7 : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.MealDishes", newName: "DishMeals");
            DropPrimaryKey("dbo.DishMeals");
            CreateTable(
                "dbo.Days",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            AddColumn("dbo.Meals", "Day_ID", c => c.Int());
            AddPrimaryKey("dbo.DishMeals", new[] { "Dish_ID", "Meal_ID" });
            CreateIndex("dbo.Meals", "Day_ID");
            AddForeignKey("dbo.Meals", "Day_ID", "dbo.Days", "ID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Meals", "Day_ID", "dbo.Days");
            DropIndex("dbo.Meals", new[] { "Day_ID" });
            DropPrimaryKey("dbo.DishMeals");
            DropColumn("dbo.Meals", "Day_ID");
            DropTable("dbo.Days");
            AddPrimaryKey("dbo.DishMeals", new[] { "Meal_ID", "Dish_ID" });
            RenameTable(name: "dbo.DishMeals", newName: "MealDishes");
        }
    }
}
