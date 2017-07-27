namespace PersonnelManagmentSystemV1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class testMigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Departments",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        ManagerID = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.AspNetUsers", t => t.ManagerID)
                .Index(t => t.ManagerID);
            
            CreateTable(
                "dbo.Information",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false),
                        Contents = c.String(nullable: false),
                        IsPublic = c.Boolean(nullable: false),
                        UploadTime = c.DateTime(nullable: false),
                        Department_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Departments", t => t.Department_ID)
                .Index(t => t.Department_ID);
            
            AddColumn("dbo.JobOpenings", "Department_ID", c => c.Int());
            AddColumn("dbo.AspNetUsers", "Department_ID", c => c.Int());
            CreateIndex("dbo.AspNetUsers", "Department_ID");
            CreateIndex("dbo.JobOpenings", "Department_ID");
            AddForeignKey("dbo.AspNetUsers", "Department_ID", "dbo.Departments", "ID");
            AddForeignKey("dbo.JobOpenings", "Department_ID", "dbo.Departments", "ID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Departments", "ManagerID", "dbo.AspNetUsers");
            DropForeignKey("dbo.JobOpenings", "Department_ID", "dbo.Departments");
            DropForeignKey("dbo.Information", "Department_ID", "dbo.Departments");
            DropForeignKey("dbo.AspNetUsers", "Department_ID", "dbo.Departments");
            DropIndex("dbo.JobOpenings", new[] { "Department_ID" });
            DropIndex("dbo.Information", new[] { "Department_ID" });
            DropIndex("dbo.AspNetUsers", new[] { "Department_ID" });
            DropIndex("dbo.Departments", new[] { "ManagerID" });
            DropColumn("dbo.AspNetUsers", "Department_ID");
            DropColumn("dbo.JobOpenings", "Department_ID");
            DropTable("dbo.Information");
            DropTable("dbo.Departments");
        }
    }
}
