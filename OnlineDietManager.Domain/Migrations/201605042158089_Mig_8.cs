namespace OnlineDietManager.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Mig_8 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Days", "Course_ID", "dbo.Courses");
            DropIndex("dbo.Days", new[] { "Course_ID" });
            RenameColumn(table: "dbo.Days", name: "Course_ID", newName: "CourseID");
            AlterColumn("dbo.Days", "CourseID", c => c.Int(nullable: false));
            CreateIndex("dbo.Days", "CourseID");
            AddForeignKey("dbo.Days", "CourseID", "dbo.Courses", "ID", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Days", "CourseID", "dbo.Courses");
            DropIndex("dbo.Days", new[] { "CourseID" });
            AlterColumn("dbo.Days", "CourseID", c => c.Int());
            RenameColumn(table: "dbo.Days", name: "CourseID", newName: "Course_ID");
            CreateIndex("dbo.Days", "Course_ID");
            AddForeignKey("dbo.Days", "Course_ID", "dbo.Courses", "ID");
        }
    }
}
