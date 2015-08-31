namespace BlogApp.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class comment_read : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Comments", "Read", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Comments", "Read");
        }
    }
}
