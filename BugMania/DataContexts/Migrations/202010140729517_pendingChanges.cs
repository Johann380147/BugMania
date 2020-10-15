namespace BugMania.DataContexts.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class pendingChanges : DbMigration
    {
        public override void Up()
        {
            //AlterColumn("dbo.BugReports", "LastEditDateTime", c => c.DateTime());
        }
        
        public override void Down()
        {
            //AlterColumn("dbo.BugReports", "LastEditDateTime", c => c.DateTime(nullable: false));
        }
    }
}
