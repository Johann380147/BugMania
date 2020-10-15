namespace BugMania.DataContexts.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddLogShape : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Logs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        BugReportId = c.Int(nullable: false),
                        EditorId = c.String(nullable: false, maxLength: 128),
                        EditDateTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.BugReports", t => t.BugReportId)
                .ForeignKey("dbo.AspNetUsers", t => t.EditorId)
                .Index(t => t.BugReportId)
                .Index(t => t.EditorId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Logs", "EditorId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Logs", "BugReportId", "dbo.BugReports");
            DropIndex("dbo.Logs", new[] { "EditorId" });
            DropIndex("dbo.Logs", new[] { "BugReportId" });
            DropTable("dbo.Logs");
        }
    }
}
