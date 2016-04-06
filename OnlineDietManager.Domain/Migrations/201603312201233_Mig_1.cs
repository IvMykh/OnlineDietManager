namespace OnlineDietManager.Domain.Practice.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Mig_1 : DbMigration
    {
        public override void Up()
        {
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
                .Index(t => t.DishRefID);
            
            CreateTable(
                "dbo.Dishes",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Protein = c.Single(nullable: false),
                        Fat = c.Single(nullable: false),
                        Carbohydrates = c.Single(nullable: false),
                        Caloricity = c.Single(nullable: false),
                        Weight = c.Single(nullable: false),
                        Name = c.String(nullable: false),
                        Description = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.DishComponents", "DishRefID", "dbo.Dishes");
            DropIndex("dbo.DishComponents", new[] { "DishRefID" });
            DropTable("dbo.Dishes");
            DropTable("dbo.DishComponents");
        }
    }
}
