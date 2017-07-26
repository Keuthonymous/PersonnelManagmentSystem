namespace PersonnelManagmentSystemV1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ModelAdjustment : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.AspNetUsers", "Job_ID", "dbo.Jobs");
            DropIndex("dbo.AspNetUsers", new[] { "Job_ID" });
            CreateTable(
                "dbo.JobOpenings",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false),
                        Description = c.String(nullable: false),
                        JobType = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            AddColumn("dbo.AspNetUsers", "JobOpening_ID", c => c.Int());
            CreateIndex("dbo.AspNetUsers", "JobOpening_ID");
            AddForeignKey("dbo.AspNetUsers", "JobOpening_ID", "dbo.JobOpenings", "ID");
            DropColumn("dbo.AspNetUsers", "Job_ID");
            DropTable("dbo.Jobs");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Jobs",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false),
                        Description = c.String(nullable: false),
                        CompName = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            AddColumn("dbo.AspNetUsers", "Job_ID", c => c.Int());
            DropForeignKey("dbo.AspNetUsers", "JobOpening_ID", "dbo.JobOpenings");
            DropIndex("dbo.AspNetUsers", new[] { "JobOpening_ID" });
            DropColumn("dbo.AspNetUsers", "JobOpening_ID");
            DropTable("dbo.JobOpenings");
            CreateIndex("dbo.AspNetUsers", "Job_ID");
            AddForeignKey("dbo.AspNetUsers", "Job_ID", "dbo.Jobs", "ID");
        }
    }
}
