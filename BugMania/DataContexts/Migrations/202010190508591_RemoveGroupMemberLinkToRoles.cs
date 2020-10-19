namespace BugMania.DataContexts.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveGroupMemberLinkToRoles : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.GroupMemberRoles", "GroupMemberId", "dbo.GroupMembers");
            DropForeignKey("dbo.GroupMemberRoles", "RoleId", "dbo.Roles");
            DropIndex("dbo.GroupMemberRoles", new[] { "GroupMemberId" });
            DropIndex("dbo.GroupMemberRoles", new[] { "RoleId" });
            DropTable("dbo.GroupMemberRoles");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.GroupMemberRoles",
                c => new
                    {
                        GroupMemberId = c.Int(nullable: false),
                        RoleId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.GroupMemberId, t.RoleId });
            
            CreateIndex("dbo.GroupMemberRoles", "RoleId");
            CreateIndex("dbo.GroupMemberRoles", "GroupMemberId");
            AddForeignKey("dbo.GroupMemberRoles", "RoleId", "dbo.Roles", "Id");
            AddForeignKey("dbo.GroupMemberRoles", "GroupMemberId", "dbo.GroupMembers", "Id");
        }
    }
}
