namespace BugMania.DataContexts.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeGroupMemberToJoinTableForUserToGroups : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.GroupMembers", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.GroupMembers", "GroupId", "dbo.Groups");
            DropForeignKey("dbo.Groups", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropIndex("dbo.Groups", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.GroupMembers", new[] { "UserId" });
            DropTable("dbo.GroupMembers");
            CreateTable(
                "dbo.GroupMembers",
                c => new
                    {
                        GroupId = c.Int(nullable: false),
                        MemberId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.GroupId, t.MemberId })
                .ForeignKey("dbo.Groups", t => t.GroupId)
                .ForeignKey("dbo.AspNetUsers", t => t.MemberId)
                .Index(t => t.MemberId);
            
            DropColumn("dbo.Groups", "ApplicationUser_Id");
            
        }
        
        public override void Down()
        {
            DropTable("dbo.GroupMembers");
            CreateTable(
                "dbo.GroupMembers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        GroupId = c.Int(nullable: false),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Groups", "ApplicationUser_Id", c => c.String(maxLength: 128));
            DropForeignKey("dbo.GroupMembers", "MemberId", "dbo.AspNetUsers");
            DropForeignKey("dbo.GroupMembers", "GroupId", "dbo.Groups");
            DropIndex("dbo.GroupMembers", new[] { "MemberId" });
            
            CreateIndex("dbo.GroupMembers", "UserId");
            CreateIndex("dbo.Groups", "ApplicationUser_Id");
            AddForeignKey("dbo.Groups", "ApplicationUser_Id", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.GroupMembers", "GroupId", "dbo.Groups", "Id");
            AddForeignKey("dbo.GroupMembers", "UserId", "dbo.AspNetUsers", "Id", cascadeDelete: true);
        }
    }
}
