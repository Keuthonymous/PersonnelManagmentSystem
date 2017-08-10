namespace PersonnelManagmentSystemV1.Migratio7ns
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Complete_2 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Information", "Department_ID", "dbo.Departments");
            DropIndex("dbo.Information", new[] { "Department_ID" });
            AlterColumn("dbo.Information", "Department_ID", c => c.Int(nullable: false));
            CreateIndex("dbo.Information", "Department_ID");
            AddForeignKey("dbo.Information", "Department_ID", "dbo.Departments", "ID", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Information", "Department_ID", "dbo.Departments");
            DropIndex("dbo.Information", new[] { "Department_ID" });
            AlterColumn("dbo.Information", "Department_ID", c => c.Int());
            CreateIndex("dbo.Information", "Department_ID");
            AddForeignKey("dbo.Information", "Department_ID", "dbo.Departments", "ID");
        }
    }
}
