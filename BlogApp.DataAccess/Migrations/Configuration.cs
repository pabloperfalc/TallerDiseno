namespace BlogApp.DataAccess.Migrations
{
    using BlogApp.Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<BlogApp.DataAccess.BlogContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(BlogApp.DataAccess.BlogContext context)
        {
            context.Roles.AddOrUpdate(
              r => r.Type,
              new Role { Description = "Administrator", Type= RoleType.Administrator },
              new Role { Description = "Blogger", Type= RoleType.Blogger }
            );
            
        }
    }
}
