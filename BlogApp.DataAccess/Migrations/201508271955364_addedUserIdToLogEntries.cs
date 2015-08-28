namespace BlogApp.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedUserIdToLogEntries : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.LogEntries", "UserId", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.LogEntries", "UserId");
        }
    }
}
