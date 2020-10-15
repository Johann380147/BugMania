namespace BugMania.DataContexts.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeGroupMemberRelationship : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.GroupMemberMap", "GroupId", "dbo.Groups");
            DropForeignKey("dbo.GroupMemberMap", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Groups", "GroupMember_Id", "dbo.GroupMembers");
            DropForeignKey("dbo.AspNetUsers", "GroupMember_Id", "dbo.GroupMembers");
            DropIndex("dbo.AspNetUsers", new[] { "GroupMember_Id" });
            DropIndex("dbo.Groups", new[] { "GroupMember_Id" });
            DropIndex("dbo.GroupMemberMap", new[] { "GroupId" });
            DropIndex("dbo.GroupMemberMap", new[] { "UserId" });
            AddColumn("dbo.Groups", "ApplicationUser_Id", c => c.String(maxLength: 128));
            AlterColumn("dbo.GroupMembers", "GroupId", c => c.Int(nullable: false));
            AlterColumn("dbo.GroupMembers", "UserId", c => c.String(nullable: false, maxLength: 128));
            CreateIndex("dbo.Groups", "ApplicationUser_Id");
            CreateIndex("dbo.GroupMembers", "GroupId");
            CreateIndex("dbo.GroupMembers", "UserId");
            AddForeignKey("dbo.GroupMembers", "UserId", "dbo.AspNetUsers", "Id", cascadeDelete: true);
            AddForeignKey("dbo.GroupMembers", "GroupId", "dbo.Groups", "Id");
            AddForeignKey("dbo.Groups", "ApplicationUser_Id", "dbo.AspNetUsers", "Id");
            DropColumn("dbo.AspNetUsers", "GroupMember_Id");
            DropColumn("dbo.Groups", "GroupMember_Id");
            DropTable("dbo.GroupMemberMap");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.GroupMemberMap",
                c => new
                    {
                        GroupId = c.Int(nullable: false),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.GroupId, t.UserId });
            
            AddColumn("dbo.Groups", "GroupMember_Id", c => c.Int());
            AddColumn("dbo.AspNetUsers", "GroupMember_Id", c => c.Int());
            DropForeignKey("dbo.Groups", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.GroupMembers", "GroupId", "dbo.Groups");
            DropForeignKey("dbo.GroupMembers", "UserId", "dbo.AspNetUsers");
            DropIndex("dbo.GroupMembers", new[] { "UserId" });
            DropIndex("dbo.GroupMembers", new[] { "GroupId" });
            DropIndex("dbo.Groups", new[] { "ApplicationUser_Id" });
            AlterColumn("dbo.GroupMembers", "UserId", c => c.String(nullable: false));
            AlterColumn("dbo.GroupMembers", "GroupId", c => c.String(nullable: false));
            DropColumn("dbo.Groups", "ApplicationUser_Id");
            CreateIndex("dbo.GroupMemberMap", "UserId");
            CreateIndex("dbo.GroupMemberMap", "GroupId");
            CreateIndex("dbo.Groups", "GroupMember_Id");
            CreateIndex("dbo.AspNetUsers", "GroupMember_Id");
            AddForeignKey("dbo.AspNetUsers", "GroupMember_Id", "dbo.GroupMembers", "Id");
            AddForeignKey("dbo.Groups", "GroupMember_Id", "dbo.GroupMembers", "Id");
            AddForeignKey("dbo.GroupMemberMap", "UserId", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.GroupMemberMap", "GroupId", "dbo.Groups", "Id");
        }
    }
}
