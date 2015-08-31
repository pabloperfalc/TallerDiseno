namespace BlogApp.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class videoName : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Videos", "Name", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Videos", "Name");
        }
    }
}
