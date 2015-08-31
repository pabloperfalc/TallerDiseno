namespace BlogApp.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class authoridcomments : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Comments", name: "User_Id", newName: "AuthorId");
            RenameIndex(table: "dbo.Comments", name: "IX_User_Id", newName: "IX_AuthorId");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.Comments", name: "IX_AuthorId", newName: "IX_User_Id");
            RenameColumn(table: "dbo.Comments", name: "AuthorId", newName: "User_Id");
        }
    }
}
