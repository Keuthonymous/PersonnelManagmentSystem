namespace PersonnelManagmentSystemV1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Incremental : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Messages", "FirstMessageInThread_ID", "dbo.Messages");
            DropForeignKey("dbo.AspNetUsers", "Department_ID", "dbo.Departments");
            DropIndex("dbo.AspNetUsers", new[] { "Department_ID" });
            DropIndex("dbo.Messages", new[] { "FirstMessageInThread_ID" });
            RenameColumn(table: "dbo.AspNetUsers", name: "Department_ID", newName: "DepartmentID");
            CreateTable(
                "dbo.Calenders",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        DepartmentID = c.Int(nullable: false),
                        CalTitle = c.String(maxLength: 25),
                        CalContent = c.String(maxLength: 200),
                        CalenderStart = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        CalenderEnd = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Departments", t => t.DepartmentID, cascadeDelete: true)
                .Index(t => t.DepartmentID);
            
            AddColumn("dbo.Messages", "FirstMessageInThreadID", c => c.Int(nullable: false));
            AlterColumn("dbo.AspNetUsers", "DepartmentID", c => c.Int(nullable: true));
            CreateIndex("dbo.AspNetUsers", "DepartmentID");
            AddForeignKey("dbo.AspNetUsers", "DepartmentID", "dbo.Departments", "ID", cascadeDelete: true);
            DropColumn("dbo.Messages", "FirstMessageInThread_ID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Messages", "FirstMessageInThread_ID", c => c.Int());
            DropForeignKey("dbo.AspNetUsers", "DepartmentID", "dbo.Departments");
            DropForeignKey("dbo.Calenders", "DepartmentID", "dbo.Departments");
            DropIndex("dbo.AspNetUsers", new[] { "DepartmentID" });
            DropIndex("dbo.Calenders", new[] { "DepartmentID" });
            AlterColumn("dbo.AspNetUsers", "DepartmentID", c => c.Int());
            DropColumn("dbo.Messages", "FirstMessageInThreadID");
            DropTable("dbo.Calenders");
            RenameColumn(table: "dbo.AspNetUsers", name: "DepartmentID", newName: "Department_ID");
            CreateIndex("dbo.Messages", "FirstMessageInThread_ID");
            CreateIndex("dbo.AspNetUsers", "Department_ID");
            AddForeignKey("dbo.AspNetUsers", "Department_ID", "dbo.Departments", "ID");
            AddForeignKey("dbo.Messages", "FirstMessageInThread_ID", "dbo.Messages", "ID");
        }
    }
}
