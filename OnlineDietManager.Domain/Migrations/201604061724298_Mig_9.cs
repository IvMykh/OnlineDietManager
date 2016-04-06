namespace OnlineDietManager.Domain.Practice.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Mig_9 : DbMigration
    {
        public override void Up()
        {
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
            
            DropColumn("dbo.Courses", "StartDate");
            DropColumn("dbo.Courses", "Discriminator");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Courses", "Discriminator", c => c.String(nullable: false, maxLength: 128));
            AddColumn("dbo.Courses", "StartDate", c => c.DateTime());
            DropForeignKey("dbo.ActiveCourses", "ID", "dbo.Courses");
            DropIndex("dbo.ActiveCourses", new[] { "ID" });
            DropTable("dbo.ActiveCourses");
        }
    }
}
