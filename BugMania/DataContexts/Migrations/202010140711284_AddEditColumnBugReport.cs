namespace BugMania.DataContexts.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddEditColumnBugReport : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.BugReports", "LastEditDateTime", c => c.DateTime(nullable: true));
        }
        
        public override void Down()
        {
            DropColumn("dbo.BugReports", "LastEditDateTime");
        }
    }
}
