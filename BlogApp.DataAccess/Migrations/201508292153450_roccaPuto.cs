namespace BlogApp.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class roccaPuto : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Comments", "ModificationdDate");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Comments", "ModificationdDate", c => c.DateTime(nullable: false));
        }
    }
}
