namespace BlogApp.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CambiosEnUser : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Comments", "User_Id", c => c.Int());
            CreateIndex("dbo.Comments", "User_Id");
            AddForeignKey("dbo.Comments", "User_Id", "dbo.Users", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Comments", "User_Id", "dbo.Users");
            DropIndex("dbo.Comments", new[] { "User_Id" });
            DropColumn("dbo.Comments", "User_Id");
        }
    }
}
