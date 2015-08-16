namespace BlogApp.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class paswod : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "Password", c => c.String(nullable: false));
            AlterColumn("dbo.Users", "Username", c => c.String(nullable: false, maxLength: 12));
            DropColumn("dbo.Users", "Passwod");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Users", "Passwod", c => c.String());
            AlterColumn("dbo.Users", "Username", c => c.String(maxLength: 12));
            DropColumn("dbo.Users", "Password");
        }
    }
}
