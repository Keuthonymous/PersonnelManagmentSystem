namespace PersonnelManagmentSystemV1.Migratio7ns
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Complete : DbMigration
    {
        public override void Up()
        {
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
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                        Department_ID = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Departments", t => t.Department_ID)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex")
                .Index(t => t.Department_ID);
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.CVs",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false),
                        Description = c.String(nullable: false),
                        MimeType = c.String(nullable: false),
                        Content = c.Binary(nullable: false),
                        UploadTime = c.DateTime(nullable: false),
                        FileName = c.String(),
                        Uploader_Id = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.AspNetUsers", t => t.Uploader_Id, cascadeDelete: true)
                .Index(t => t.Uploader_Id);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Messages",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        FirstMessageInThreadID = c.Int(nullable: false),
                        Title = c.String(nullable: false, maxLength: 200),
                        BodyContent = c.String(nullable: false),
                        SendTime = c.DateTime(nullable: false),
                        JobOpening_ID = c.Int(),
                        Recipient_Id = c.String(maxLength: 128),
                        Sender_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.JobOpenings", t => t.JobOpening_ID)
                .ForeignKey("dbo.AspNetUsers", t => t.Recipient_Id)
                .ForeignKey("dbo.AspNetUsers", t => t.Sender_Id)
                .Index(t => t.JobOpening_ID)
                .Index(t => t.Recipient_Id)
                .Index(t => t.Sender_Id);
            
            CreateTable(
                "dbo.JobOpenings",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false),
                        Description = c.String(nullable: false),
                        JobType = c.String(nullable: false),
                        Department_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Departments", t => t.Department_ID)
                .Index(t => t.Department_ID);
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.UserFiles",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false),
                        Description = c.String(nullable: false),
                        MimeType = c.String(nullable: false),
                        OriginalFilename = c.String(),
                        Content = c.Binary(nullable: false),
                        Department_ID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Departments", t => t.Department_ID, cascadeDelete: true)
                .Index(t => t.Department_ID);
            
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
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.Departments", "ManagerID", "dbo.AspNetUsers");
            DropForeignKey("dbo.JobOpenings", "Department_ID", "dbo.Departments");
            DropForeignKey("dbo.Information", "Department_ID", "dbo.Departments");
            DropForeignKey("dbo.UserFiles", "Department_ID", "dbo.Departments");
            DropForeignKey("dbo.AspNetUsers", "Department_ID", "dbo.Departments");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Messages", "Sender_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Messages", "Recipient_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Messages", "JobOpening_ID", "dbo.JobOpenings");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.CVs", "Uploader_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Calenders", "DepartmentID", "dbo.Departments");
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.Information", new[] { "Department_ID" });
            DropIndex("dbo.UserFiles", new[] { "Department_ID" });
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.JobOpenings", new[] { "Department_ID" });
            DropIndex("dbo.Messages", new[] { "Sender_Id" });
            DropIndex("dbo.Messages", new[] { "Recipient_Id" });
            DropIndex("dbo.Messages", new[] { "JobOpening_ID" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.CVs", new[] { "Uploader_Id" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", new[] { "Department_ID" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.Departments", new[] { "ManagerID" });
            DropIndex("dbo.Calenders", new[] { "DepartmentID" });
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.Information");
            DropTable("dbo.UserFiles");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.JobOpenings");
            DropTable("dbo.Messages");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.CVs");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.Departments");
            DropTable("dbo.Calenders");
        }
    }
}
