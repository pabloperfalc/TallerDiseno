namespace BlogApp.Logger.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class final_log : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.LogEntries",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Date = c.DateTime(nullable: false),
                        Thread = c.String(),
                        Level = c.String(),
                        LogType = c.String(),
                        UserUsername = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.LogEntries");
        }
    }
}
