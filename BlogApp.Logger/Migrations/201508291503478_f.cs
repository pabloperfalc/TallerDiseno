namespace BlogApp.Logger.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class f : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.LogEntries", "UserUsername", c => c.String());
            DropColumn("dbo.LogEntries", "UserId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.LogEntries", "UserId", c => c.String());
            DropColumn("dbo.LogEntries", "UserUsername");
        }
    }
}
