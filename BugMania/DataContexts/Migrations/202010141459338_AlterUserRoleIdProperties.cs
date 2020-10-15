namespace BugMania.DataContexts.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AlterUserRoleIdProperties : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.AspNetUsers", "RoleId", c => c.Int(nullable:true));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.AspNetUsers", "RoleId", c => c.Int(defaultValue: 2));
        }
    }
}
