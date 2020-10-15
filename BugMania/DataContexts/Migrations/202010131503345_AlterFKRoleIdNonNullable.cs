namespace BugMania.DataContexts.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AlterFKRoleIdNonNullable : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.AspNetUsers", "RoleId", c => c.Int(defaultValue: 2));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.AspNetUsers", "RoleId", c => c.Int(nullable: true));
        }
    }
}
