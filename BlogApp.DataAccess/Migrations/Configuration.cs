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
              r => r.Description,
              new Role { Description = "Blogger" },
              new Role { Description = "Administrator" }
            );
            
        }
    }
}
