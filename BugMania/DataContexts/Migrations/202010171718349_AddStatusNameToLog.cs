namespace BugMania.DataContexts.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddStatusNameToLog : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Logs", "Status", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Logs", "Status");
        }
    }
}
