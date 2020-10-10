namespace BugMania.DataContexts.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddCommentRelationToUser : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.GroupMembers", "GroupId", c => c.String(nullable: false));
            AlterColumn("dbo.GroupMembers", "UserId", c => c.String(nullable: false));
            AlterColumn("dbo.Roles", "Name", c => c.String(nullable: false));
            CreateIndex("dbo.Comments", "UserId");
            AddForeignKey("dbo.Comments", "UserId", "dbo.AspNetUsers", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Comments", "UserId", "dbo.AspNetUsers");
            DropIndex("dbo.Comments", new[] { "UserId" });
            AlterColumn("dbo.Roles", "Name", c => c.String());
            AlterColumn("dbo.GroupMembers", "UserId", c => c.String());
            AlterColumn("dbo.GroupMembers", "GroupId", c => c.String());
        }
    }
}
