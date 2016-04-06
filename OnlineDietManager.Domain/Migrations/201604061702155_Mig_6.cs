namespace OnlineDietManager.Domain.Practice.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Mig_6 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.MealDishes", "DishID", "dbo.Dishes");
            DropForeignKey("dbo.Dishes", "Meal_ID", "dbo.Meals");
            DropForeignKey("dbo.MealDishes", "MealID", "dbo.Meals");
            DropIndex("dbo.Dishes", new[] { "Meal_ID" });
            DropIndex("dbo.MealDishes", new[] { "MealID" });
            DropIndex("dbo.MealDishes", new[] { "DishID" });
            CreateTable(
                "dbo.MealDishes",
                c => new
                    {
                        Meal_ID = c.Int(nullable: false),
                        Dish_ID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Meal_ID, t.Dish_ID })
                .ForeignKey("dbo.Meals", t => t.Meal_ID, cascadeDelete: true)
                .ForeignKey("dbo.Dishes", t => t.Dish_ID, cascadeDelete: true)
                .Index(t => t.Meal_ID)
                .Index(t => t.Dish_ID);
            
            DropColumn("dbo.Dishes", "Meal_ID");
            //DropTable("dbo.MealDishes");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.MealDishes",
                c => new
                    {
                        MealID = c.Int(nullable: false),
                        DishID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.MealID, t.DishID });
            
            AddColumn("dbo.Dishes", "Meal_ID", c => c.Int());
            DropForeignKey("dbo.MealDishes", "Dish_ID", "dbo.Dishes");
            DropForeignKey("dbo.MealDishes", "Meal_ID", "dbo.Meals");
            DropIndex("dbo.MealDishes", new[] { "Dish_ID" });
            DropIndex("dbo.MealDishes", new[] { "Meal_ID" });
            DropTable("dbo.MealDishes");
            CreateIndex("dbo.MealDishes", "DishID");
            CreateIndex("dbo.MealDishes", "MealID");
            CreateIndex("dbo.Dishes", "Meal_ID");
            AddForeignKey("dbo.MealDishes", "MealID", "dbo.Meals", "ID", cascadeDelete: true);
            AddForeignKey("dbo.Dishes", "Meal_ID", "dbo.Meals", "ID");
            AddForeignKey("dbo.MealDishes", "DishID", "dbo.Dishes", "ID", cascadeDelete: true);
        }
    }
}
