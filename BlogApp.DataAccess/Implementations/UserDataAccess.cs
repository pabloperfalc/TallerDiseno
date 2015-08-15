using BlogApp.Manager.RequiredInterfaces;
using BlogApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BlogApp.DataAccess.Implementations
{
    public class UserDataAccess : IUserDataAccess
    {
        public void AddUser(User user) 
        {
            try
            {
                using (var db = new BlogContext())
                {
                    db.Users.Add(user);
                    db.SaveChanges();

                }
            }
            catch (Exception)
            {
                //capturar
            }
        
        }

        public void ModifyUser(User user) 
        {
            
        }

        public void RemoveUser(User user) 
        {
            try
            {
                using (var db = new BlogContext())
                {
                    db.Users.Attach(user);
                    user.IsActive = false;
                    db.SaveChanges();

                }
            }
            catch (Exception)
            {

            }
        }
        public List<User> GetUsers() 
        {
            try
            {
                using (var db = new BlogContext())
                {
                    var query = from u in db.Users
                                where u.IsActive == true
                                select u;
                    return query.ToList<User>();
                }
            }
            catch (Exception)
            {
                return new List<User>();
            }
        }
    }
}
