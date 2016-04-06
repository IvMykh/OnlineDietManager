namespace OnlineDietManager.Domain.Practice.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Ingredients",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Protein = c.Single(nullable: false),
                        Fat = c.Single(nullable: false),
                        Carbohydrates = c.Single(nullable: false),
                        Caloricity = c.Single(nullable: false),
                        Name = c.String(nullable: false),
                        Description = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Ingredients");
        }
    }
}
