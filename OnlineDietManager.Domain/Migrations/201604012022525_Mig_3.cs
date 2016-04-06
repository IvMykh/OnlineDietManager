namespace OnlineDietManager.Domain.Practice.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Mig_3 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Dishes", "Protein");
            DropColumn("dbo.Dishes", "Fat");
            DropColumn("dbo.Dishes", "Carbohydrates");
            DropColumn("dbo.Dishes", "Caloricity");
            DropColumn("dbo.Dishes", "Weight");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Dishes", "Weight", c => c.Single(nullable: false));
            AddColumn("dbo.Dishes", "Caloricity", c => c.Single(nullable: false));
            AddColumn("dbo.Dishes", "Carbohydrates", c => c.Single(nullable: false));
            AddColumn("dbo.Dishes", "Fat", c => c.Single(nullable: false));
            AddColumn("dbo.Dishes", "Protein", c => c.Single(nullable: false));
        }
    }
}
