namespace BugMania.DataContexts.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.BugReports",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false, maxLength: 256),
                        Description = c.String(nullable: false),
                        ProductId = c.Int(nullable: false),
                        AuthorId = c.String(nullable: false, maxLength: 128),
                        SeverityId = c.Int(nullable: false),
                        PriorityId = c.Int(nullable: false),
                        StatusId = c.Int(nullable: false),
                        CreateDateTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.AuthorId)
                .ForeignKey("dbo.Priority", t => t.PriorityId)
                .ForeignKey("dbo.Products", t => t.ProductId)
                .ForeignKey("dbo.Severity", t => t.SeverityId)
                .ForeignKey("dbo.Status", t => t.StatusId)
                .Index(t => t.ProductId)
                .Index(t => t.AuthorId)
                .Index(t => t.SeverityId)
                .Index(t => t.PriorityId)
                .Index(t => t.StatusId);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        isAdmin = c.Boolean(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
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
                        GroupMember_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.GroupMembers", t => t.GroupMember_Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex")
                .Index(t => t.GroupMember_Id);
            
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
                "dbo.Groups",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 256),
                        ProductId = c.Int(nullable: false),
                        GroupMember_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Products", t => t.ProductId)
                .ForeignKey("dbo.GroupMembers", t => t.GroupMember_Id)
                .Index(t => t.ProductId)
                .Index(t => t.GroupMember_Id);
            
            CreateTable(
                "dbo.Products",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 256),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
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
                "dbo.Comments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        BugReportId = c.Int(nullable: false),
                        Content = c.String(nullable: false),
                        CommentDateTime = c.DateTime(nullable: false),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.BugReports", t => t.BugReportId, cascadeDelete: true)
                .Index(t => t.BugReportId);
            
            CreateTable(
                "dbo.Priority",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 256),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Severity",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 256),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Status",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 256),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Tags",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 256),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true);
            
            CreateTable(
                "dbo.GroupMembers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        GroupId = c.String(),
                        UserId = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Roles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
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
                "dbo.GroupMemberMap",
                c => new
                    {
                        GroupId = c.Int(nullable: false),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.GroupId, t.UserId })
                .ForeignKey("dbo.Groups", t => t.GroupId)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.GroupId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.BugReportAssignees",
                c => new
                    {
                        BugReportId = c.Int(nullable: false),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.BugReportId, t.UserId })
                .ForeignKey("dbo.BugReports", t => t.BugReportId)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.BugReportId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.BugReportSubscribers",
                c => new
                    {
                        BugReportId = c.Int(nullable: false),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.BugReportId, t.UserId })
                .ForeignKey("dbo.BugReports", t => t.BugReportId)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.BugReportId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.BugReportTags",
                c => new
                    {
                        BugReportId = c.Int(nullable: false),
                        TagId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.BugReportId, t.TagId })
                .ForeignKey("dbo.BugReports", t => t.BugReportId)
                .ForeignKey("dbo.Tags", t => t.TagId)
                .Index(t => t.BugReportId)
                .Index(t => t.TagId);
            
            CreateTable(
                "dbo.GroupMemberRoles",
                c => new
                    {
                        GroupMemberId = c.Int(nullable: false),
                        RoleId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.GroupMemberId, t.RoleId })
                .ForeignKey("dbo.GroupMembers", t => t.GroupMemberId)
                .ForeignKey("dbo.Roles", t => t.RoleId)
                .Index(t => t.GroupMemberId)
                .Index(t => t.RoleId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.GroupMemberRoles", "RoleId", "dbo.Roles");
            DropForeignKey("dbo.GroupMemberRoles", "GroupMemberId", "dbo.GroupMembers");
            DropForeignKey("dbo.AspNetUsers", "GroupMember_Id", "dbo.GroupMembers");
            DropForeignKey("dbo.Groups", "GroupMember_Id", "dbo.GroupMembers");
            DropForeignKey("dbo.BugReportTags", "TagId", "dbo.Tags");
            DropForeignKey("dbo.BugReportTags", "BugReportId", "dbo.BugReports");
            DropForeignKey("dbo.BugReportSubscribers", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.BugReportSubscribers", "BugReportId", "dbo.BugReports");
            DropForeignKey("dbo.BugReports", "StatusId", "dbo.Status");
            DropForeignKey("dbo.BugReports", "SeverityId", "dbo.Severity");
            DropForeignKey("dbo.BugReports", "ProductId", "dbo.Products");
            DropForeignKey("dbo.BugReports", "PriorityId", "dbo.Priority");
            DropForeignKey("dbo.Comments", "BugReportId", "dbo.BugReports");
            DropForeignKey("dbo.BugReports", "AuthorId", "dbo.AspNetUsers");
            DropForeignKey("dbo.BugReportAssignees", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.BugReportAssignees", "BugReportId", "dbo.BugReports");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Groups", "ProductId", "dbo.Products");
            DropForeignKey("dbo.GroupMemberMap", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.GroupMemberMap", "GroupId", "dbo.Groups");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropIndex("dbo.GroupMemberRoles", new[] { "RoleId" });
            DropIndex("dbo.GroupMemberRoles", new[] { "GroupMemberId" });
            DropIndex("dbo.BugReportTags", new[] { "TagId" });
            DropIndex("dbo.BugReportTags", new[] { "BugReportId" });
            DropIndex("dbo.BugReportSubscribers", new[] { "UserId" });
            DropIndex("dbo.BugReportSubscribers", new[] { "BugReportId" });
            DropIndex("dbo.BugReportAssignees", new[] { "UserId" });
            DropIndex("dbo.BugReportAssignees", new[] { "BugReportId" });
            DropIndex("dbo.GroupMemberMap", new[] { "UserId" });
            DropIndex("dbo.GroupMemberMap", new[] { "GroupId" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.Tags", new[] { "Name" });
            DropIndex("dbo.Comments", new[] { "BugReportId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.Groups", new[] { "GroupMember_Id" });
            DropIndex("dbo.Groups", new[] { "ProductId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", new[] { "GroupMember_Id" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.BugReports", new[] { "StatusId" });
            DropIndex("dbo.BugReports", new[] { "PriorityId" });
            DropIndex("dbo.BugReports", new[] { "SeverityId" });
            DropIndex("dbo.BugReports", new[] { "AuthorId" });
            DropIndex("dbo.BugReports", new[] { "ProductId" });
            DropTable("dbo.GroupMemberRoles");
            DropTable("dbo.BugReportTags");
            DropTable("dbo.BugReportSubscribers");
            DropTable("dbo.BugReportAssignees");
            DropTable("dbo.GroupMemberMap");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.Roles");
            DropTable("dbo.GroupMembers");
            DropTable("dbo.Tags");
            DropTable("dbo.Status");
            DropTable("dbo.Severity");
            DropTable("dbo.Priority");
            DropTable("dbo.Comments");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.Products");
            DropTable("dbo.Groups");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.BugReports");
        }
    }
}
