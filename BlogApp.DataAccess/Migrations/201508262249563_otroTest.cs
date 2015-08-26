namespace BlogApp.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class otroTest : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.LogEntries", "LogType_Id", "dbo.LogEntries");
            DropIndex("dbo.LogEntries", new[] { "LogType_Id" });
            AddColumn("dbo.LogEntries", "LogType", c => c.String());
            DropColumn("dbo.LogEntries", "LogType_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.LogEntries", "LogType_Id", c => c.Int());
            DropColumn("dbo.LogEntries", "LogType");
            CreateIndex("dbo.LogEntries", "LogType_Id");
            AddForeignKey("dbo.LogEntries", "LogType_Id", "dbo.LogEntries", "Id");
        }
    }
}
