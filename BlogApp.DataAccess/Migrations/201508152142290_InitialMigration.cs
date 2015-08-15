namespace BlogApp.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialMigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Articles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Type = c.Int(nullable: false),
                        Text = c.String(),
                        Layout = c.Int(nullable: false),
                        PicturePath = c.String(),
                        CreationDate = c.DateTime(nullable: false),
                        ModificationdDate = c.DateTime(nullable: false),
                        AuthorId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.AuthorId, cascadeDelete: true)
                .Index(t => t.AuthorId);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        SureName = c.String(),
                        Username = c.String(maxLength: 12),
                        Passwod = c.String(),
                        RoleId = c.Int(nullable: false),
                        Email = c.String(),
                        PicturePath = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Roles", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.Roles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Comments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Text = c.String(),
                        CreationDate = c.DateTime(nullable: false),
                        ModificationdDate = c.DateTime(nullable: false),
                        Comment_Id = c.Int(),
                        Article_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Comments", t => t.Comment_Id)
                .ForeignKey("dbo.Articles", t => t.Article_Id)
                .Index(t => t.Comment_Id)
                .Index(t => t.Article_Id);
            
            CreateTable(
                "dbo.LogEntries",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Videos",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Path = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Comments", "Article_Id", "dbo.Articles");
            DropForeignKey("dbo.Comments", "Comment_Id", "dbo.Comments");
            DropForeignKey("dbo.Users", "RoleId", "dbo.Roles");
            DropForeignKey("dbo.Articles", "AuthorId", "dbo.Users");
            DropIndex("dbo.Comments", new[] { "Article_Id" });
            DropIndex("dbo.Comments", new[] { "Comment_Id" });
            DropIndex("dbo.Users", new[] { "RoleId" });
            DropIndex("dbo.Articles", new[] { "AuthorId" });
            DropTable("dbo.Videos");
            DropTable("dbo.LogEntries");
            DropTable("dbo.Comments");
            DropTable("dbo.Roles");
            DropTable("dbo.Users");
            DropTable("dbo.Articles");
        }
    }
}
