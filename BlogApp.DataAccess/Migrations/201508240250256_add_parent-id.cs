namespace BlogApp.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class add_parentid : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Comments", name: "Comment_Id", newName: "ParentId");
            RenameIndex(table: "dbo.Comments", name: "IX_Comment_Id", newName: "IX_ParentId");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.Comments", name: "IX_ParentId", newName: "IX_Comment_Id");
            RenameColumn(table: "dbo.Comments", name: "ParentId", newName: "Comment_Id");
        }
    }
}
