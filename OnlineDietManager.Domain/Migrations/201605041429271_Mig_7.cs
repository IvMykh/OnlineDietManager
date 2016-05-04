namespace OnlineDietManager.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Mig_7 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Days", "Course_ID", "dbo.Courses");
            DropForeignKey("dbo.ActiveCourses", "ID", "dbo.Courses");
            DropPrimaryKey("dbo.Courses");
            AlterColumn("dbo.Courses", "ID", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("dbo.Courses", "ID");
            AddForeignKey("dbo.Days", "Course_ID", "dbo.Courses", "ID");
            AddForeignKey("dbo.ActiveCourses", "ID", "dbo.Courses", "ID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ActiveCourses", "ID", "dbo.Courses");
            DropForeignKey("dbo.Days", "Course_ID", "dbo.Courses");
            DropPrimaryKey("dbo.Courses");
            AlterColumn("dbo.Courses", "ID", c => c.Int(nullable: false));
            AddPrimaryKey("dbo.Courses", "ID");
            AddForeignKey("dbo.ActiveCourses", "ID", "dbo.Courses", "ID");
            AddForeignKey("dbo.Days", "Course_ID", "dbo.Courses", "ID");
        }
    }
}
