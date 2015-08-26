namespace BlogApp.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class prueba : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.LogEntries", "Date", c => c.DateTime(nullable: false));
            AddColumn("dbo.LogEntries", "Thread", c => c.String());
            AddColumn("dbo.LogEntries", "Level", c => c.String());
            AddColumn("dbo.LogEntries", "LogType_Id", c => c.Int());
            CreateIndex("dbo.LogEntries", "LogType_Id");
            AddForeignKey("dbo.LogEntries", "LogType_Id", "dbo.LogEntries", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.LogEntries", "LogType_Id", "dbo.LogEntries");
            DropIndex("dbo.LogEntries", new[] { "LogType_Id" });
            DropColumn("dbo.LogEntries", "LogType_Id");
            DropColumn("dbo.LogEntries", "Level");
            DropColumn("dbo.LogEntries", "Thread");
            DropColumn("dbo.LogEntries", "Date");
        }
    }
}
