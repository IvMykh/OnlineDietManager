namespace OnlineDietManager.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Courses",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Description = c.String(),
                        OwnerID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Users", t => t.OwnerID)
                .Index(t => t.OwnerID);
            
            CreateTable(
                "dbo.Days",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Description = c.String(),
                        Course_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Courses", t => t.Course_ID)
                .Index(t => t.Course_ID);
            
            CreateTable(
                "dbo.Meals",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Description = c.String(),
                        Time = c.Time(nullable: false, precision: 7),
                        Day_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Days", t => t.Day_ID)
                .Index(t => t.Day_ID);
            
            CreateTable(
                "dbo.Dishes",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Description = c.String(nullable: false),
                        OwnerID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Users", t => t.OwnerID)
                .Index(t => t.OwnerID);
            
            CreateTable(
                "dbo.DishComponents",
                c => new
                    {
                        ID = c.Int(nullable: false),
                        DishRefID = c.Int(nullable: false),
                        Weight = c.Single(nullable: false),
                    })
                .PrimaryKey(t => new { t.ID, t.DishRefID })
                .ForeignKey("dbo.Dishes", t => t.DishRefID, cascadeDelete: true)
                .ForeignKey("dbo.Ingredients", t => t.ID, cascadeDelete: true)
                .Index(t => t.ID)
                .Index(t => t.DishRefID);
            
            CreateTable(
                "dbo.Ingredients",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Description = c.String(nullable: false),
                        Protein = c.Single(nullable: false),
                        Fat = c.Single(nullable: false),
                        Carbohydrates = c.Single(nullable: false),
                        Caloricity = c.Single(nullable: false),
                        OwnerID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Users", t => t.OwnerID)
                .Index(t => t.OwnerID);
            
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
            
            CreateTable(
                "dbo.DishMeals",
                c => new
                    {
                        Dish_ID = c.Int(nullable: false),
                        Meal_ID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Dish_ID, t.Meal_ID })
                .ForeignKey("dbo.Dishes", t => t.Dish_ID, cascadeDelete: true)
                .ForeignKey("dbo.Meals", t => t.Meal_ID, cascadeDelete: true)
                .Index(t => t.Dish_ID)
                .Index(t => t.Meal_ID);
            
            CreateTable(
                "dbo.ActiveCourses",
                c => new
                    {
                        ID = c.Int(nullable: false),
                        StartDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Courses", t => t.ID)
                .Index(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ActiveCourses", "ID", "dbo.Courses");
            DropForeignKey("dbo.Courses", "OwnerID", "dbo.Users");
            DropForeignKey("dbo.Days", "Course_ID", "dbo.Courses");
            DropForeignKey("dbo.Meals", "Day_ID", "dbo.Days");
            DropForeignKey("dbo.Dishes", "OwnerID", "dbo.Users");
            DropForeignKey("dbo.DishMeals", "Meal_ID", "dbo.Meals");
            DropForeignKey("dbo.DishMeals", "Dish_ID", "dbo.Dishes");
            DropForeignKey("dbo.DishComponents", "ID", "dbo.Ingredients");
            DropForeignKey("dbo.Ingredients", "OwnerID", "dbo.Users");
            DropForeignKey("dbo.DishComponents", "DishRefID", "dbo.Dishes");
            DropIndex("dbo.ActiveCourses", new[] { "ID" });
            DropIndex("dbo.DishMeals", new[] { "Meal_ID" });
            DropIndex("dbo.DishMeals", new[] { "Dish_ID" });
            DropIndex("dbo.Ingredients", new[] { "OwnerID" });
            DropIndex("dbo.DishComponents", new[] { "DishRefID" });
            DropIndex("dbo.DishComponents", new[] { "ID" });
            DropIndex("dbo.Dishes", new[] { "OwnerID" });
            DropIndex("dbo.Meals", new[] { "Day_ID" });
            DropIndex("dbo.Days", new[] { "Course_ID" });
            DropIndex("dbo.Courses", new[] { "OwnerID" });
            DropTable("dbo.ActiveCourses");
            DropTable("dbo.DishMeals");
            DropTable("dbo.Users");
            DropTable("dbo.Ingredients");
            DropTable("dbo.DishComponents");
            DropTable("dbo.Dishes");
            DropTable("dbo.Meals");
            DropTable("dbo.Days");
            DropTable("dbo.Courses");
        }
    }
}
