using BlogApp.Manager.RequiredInterfaces;
using BlogApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;

namespace BlogApp.DataAccess.Implementations
{
    public class UserDataAccess : IUserDataAccess
    {
        public void AddUser(User user) 
        {
            using (var db = new BlogContext())
            {
                db.Users.Add(user);
                db.SaveChanges();

            }
        }

        public void ModifyUser(User user) 
        {
            
        }

        public void RemoveUser(User user) 
        {
            using (var db = new BlogContext())
            {
                db.Users.Attach(user);
                user.IsActive = false;
                db.SaveChanges();

            }
        }
        public List<User> GetUsers() 
        {
            using (var db = new BlogContext())
            {
                var query = from u in db.Users
                            where u.IsActive == true
                            select u;
                return query.ToList<User>();
            }
        }

        public User GetUserByUsername(string username)
        {
            using (var db = new BlogContext())
            {
                var query = from u in db.Users.Include(u => u.Role)
                            where u.IsActive == true && u.Username.Trim() == username.Trim()
                            select u;
                return query.FirstOrDefault();
            }
        }
    }
}
