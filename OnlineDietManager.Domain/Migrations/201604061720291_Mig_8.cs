namespace OnlineDietManager.Domain.Practice.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Mig_8 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Courses",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Description = c.String(),
                        StartDate = c.DateTime(),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.ID);
            
            AddColumn("dbo.Days", "Course_ID", c => c.Int());
            CreateIndex("dbo.Days", "Course_ID");
            AddForeignKey("dbo.Days", "Course_ID", "dbo.Courses", "ID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Days", "Course_ID", "dbo.Courses");
            DropIndex("dbo.Days", new[] { "Course_ID" });
            DropColumn("dbo.Days", "Course_ID");
            DropTable("dbo.Courses");
        }
    }
}
