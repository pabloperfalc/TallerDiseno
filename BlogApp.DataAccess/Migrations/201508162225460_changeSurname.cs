namespace BlogApp.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changeSurname : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "Surname", c => c.String(nullable: false));
            AlterColumn("dbo.Users", "Name", c => c.String(nullable: false));
            AlterColumn("dbo.Users", "Email", c => c.String(nullable: false));
            DropColumn("dbo.Users", "SureName");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Users", "SureName", c => c.String());
            AlterColumn("dbo.Users", "Email", c => c.String());
            AlterColumn("dbo.Users", "Name", c => c.String());
            DropColumn("dbo.Users", "Surname");
        }
    }
}
