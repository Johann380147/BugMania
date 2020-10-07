namespace BugMania.DataContexts.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.BugReportAssignees",
                c => new
                    {
                        BugReportId = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.BugReportId, t.UserId })
                .ForeignKey("dbo.BugReport", t => t.BugReportId, cascadeDelete: true)
                .Index(t => t.BugReportId);
            
            CreateTable(
                "dbo.BugReport",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Title = c.String(nullable: false, maxLength: 256),
                        Description = c.String(nullable: false),
                        ProductId = c.String(nullable: false, maxLength: 128),
                        AuthorId = c.String(nullable: false, maxLength: 128),
                        SeverityId = c.String(maxLength: 128),
                        PriorityId = c.String(maxLength: 128),
                        StatusId = c.String(nullable: false, maxLength: 128),
                        CreateDateTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Priority", t => t.PriorityId)
                .ForeignKey("dbo.Products", t => t.ProductId)
                .ForeignKey("dbo.Severity", t => t.SeverityId)
                .ForeignKey("dbo.Status", t => t.StatusId)
                .Index(t => t.ProductId)
                .Index(t => t.SeverityId)
                .Index(t => t.PriorityId)
                .Index(t => t.StatusId);
            
            CreateTable(
                "dbo.BugReportSubscribers",
                c => new
                    {
                        BugReportId = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.BugReportId, t.UserId })
                .ForeignKey("dbo.BugReport", t => t.BugReportId, cascadeDelete: true)
                .Index(t => t.BugReportId);
            
            CreateTable(
                "dbo.Comments",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Content = c.String(nullable: false),
                        CommentDateTime = c.DateTime(nullable: false),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Priority",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Products",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Groups",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                        ProductId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Products", t => t.ProductId)
                .Index(t => t.ProductId);
            
            CreateTable(
                "dbo.GroupMembers",
                c => new
                    {
                        GroupId = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.GroupId, t.UserId })
                .ForeignKey("dbo.Groups", t => t.GroupId, cascadeDelete: true)
                .Index(t => t.GroupId);
            
            CreateTable(
                "dbo.Severity",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Status",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Tags",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.BugReportComments1",
                c => new
                    {
                        BugReportId = c.String(nullable: false, maxLength: 128),
                        CommentId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.BugReportId, t.CommentId });
            
            CreateTable(
                "dbo.BugReportTags1",
                c => new
                    {
                        BugReportId = c.String(nullable: false, maxLength: 128),
                        TagId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.BugReportId, t.TagId });
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
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
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
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
                "dbo.BugReportComments",
                c => new
                    {
                        BugReportId = c.String(nullable: false, maxLength: 128),
                        CommentId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.BugReportId, t.CommentId })
                .ForeignKey("dbo.BugReport", t => t.BugReportId, cascadeDelete: true)
                .ForeignKey("dbo.Comments", t => t.CommentId, cascadeDelete: true)
                .Index(t => t.BugReportId)
                .Index(t => t.CommentId);
            
            CreateTable(
                "dbo.BugReportTags",
                c => new
                    {
                        BugReportId = c.String(nullable: false, maxLength: 128),
                        TagId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.BugReportId, t.TagId })
                .ForeignKey("dbo.BugReport", t => t.BugReportId, cascadeDelete: true)
                .ForeignKey("dbo.Tags", t => t.TagId, cascadeDelete: true)
                .Index(t => t.BugReportId)
                .Index(t => t.TagId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.BugReportTags", "TagId", "dbo.Tags");
            DropForeignKey("dbo.BugReportTags", "BugReportId", "dbo.BugReport");
            DropForeignKey("dbo.BugReport", "StatusId", "dbo.Status");
            DropForeignKey("dbo.BugReport", "SeverityId", "dbo.Severity");
            DropForeignKey("dbo.Groups", "ProductId", "dbo.Products");
            DropForeignKey("dbo.GroupMembers", "GroupId", "dbo.Groups");
            DropForeignKey("dbo.BugReport", "ProductId", "dbo.Products");
            DropForeignKey("dbo.BugReport", "PriorityId", "dbo.Priority");
            DropForeignKey("dbo.BugReportComments", "CommentId", "dbo.Comments");
            DropForeignKey("dbo.BugReportComments", "BugReportId", "dbo.BugReport");
            DropForeignKey("dbo.BugReportSubscribers", "BugReportId", "dbo.BugReport");
            DropForeignKey("dbo.BugReportAssignees", "BugReportId", "dbo.BugReport");
            DropIndex("dbo.BugReportTags", new[] { "TagId" });
            DropIndex("dbo.BugReportTags", new[] { "BugReportId" });
            DropIndex("dbo.BugReportComments", new[] { "CommentId" });
            DropIndex("dbo.BugReportComments", new[] { "BugReportId" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.GroupMembers", new[] { "GroupId" });
            DropIndex("dbo.Groups", new[] { "ProductId" });
            DropIndex("dbo.BugReportSubscribers", new[] { "BugReportId" });
            DropIndex("dbo.BugReport", new[] { "StatusId" });
            DropIndex("dbo.BugReport", new[] { "PriorityId" });
            DropIndex("dbo.BugReport", new[] { "SeverityId" });
            DropIndex("dbo.BugReport", new[] { "ProductId" });
            DropIndex("dbo.BugReportAssignees", new[] { "BugReportId" });
            DropTable("dbo.BugReportTags");
            DropTable("dbo.BugReportComments");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.BugReportTags1");
            DropTable("dbo.BugReportComments1");
            DropTable("dbo.Tags");
            DropTable("dbo.Status");
            DropTable("dbo.Severity");
            DropTable("dbo.GroupMembers");
            DropTable("dbo.Groups");
            DropTable("dbo.Products");
            DropTable("dbo.Priority");
            DropTable("dbo.Comments");
            DropTable("dbo.BugReportSubscribers");
            DropTable("dbo.BugReport");
            DropTable("dbo.BugReportAssignees");
        }
    }
}
