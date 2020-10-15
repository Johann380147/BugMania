namespace BugMania.DataContexts.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddOperationTypeToLogs : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Operations",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Type = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Logs", "OperationId", c => c.Int(nullable: true));
            CreateIndex("dbo.Logs", "OperationId");
            AddForeignKey("dbo.Logs", "OperationId", "dbo.Operations", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Logs", "OperationId", "dbo.Operations");
            DropIndex("dbo.Logs", new[] { "OperationId" });
            DropColumn("dbo.Logs", "OperationId");
            DropTable("dbo.Operations");
        }
    }
}
