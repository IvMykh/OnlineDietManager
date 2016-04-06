namespace OnlineDietManager.Domain.Practice.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Mig_4 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Login = c.String(nullable: false),
                        Password = c.String(nullable: false),
                        IsAdmin = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
                //.Index(t => t.Login, unique: true);
            
        }
        
        public override void Down()
        {
            DropIndex("dbo.Users", new[] { "Login" });
            DropTable("dbo.Users");
        }
    }
}
