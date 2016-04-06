namespace OnlineDietManager.Domain.Practice.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Mig_11 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Courses", "OwnerID", c => c.Int(nullable: false));
            AddColumn("dbo.Dishes", "OwnerID", c => c.Int(nullable: false));
            AddColumn("dbo.Ingredients", "OwnerID", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Ingredients", "OwnerID");
            DropColumn("dbo.Dishes", "OwnerID");
            DropColumn("dbo.Courses", "OwnerID");
        }
    }
}
