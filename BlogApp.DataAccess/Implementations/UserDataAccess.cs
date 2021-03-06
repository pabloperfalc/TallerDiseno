﻿using BlogApp.Manager.RequiredInterfaces;
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
                foreach (Role r in user.Roles)
                {
                    db.Roles.Attach(r);
                }
                db.Users.Add(user);
                db.SaveChanges();
            }
        }

        public void ModifyUser(User user)
        {
            using (var db = new BlogContext())
            {
                var query = (from u in db.Users.Include(r=>r.Roles)
                             where u.IsActive == true && u.Id == user.Id
                             select u).FirstOrDefault<User>();


                var types = user.Roles.Select(r => r.Type);

                var newRoles = db.Roles
                              .Where(r => types.Contains(r.Type)).ToList();
                query.Email = user.Email;
                query.Name = user.Name;
                query.Surname = user.Surname;
                query.Password = user.Password;
                query.PicturePath = user.PicturePath;
                query.Roles.Clear();
                query.Roles.AddRange(newRoles);
                db.SaveChanges();
            }
        }

        public void RemoveUser(User user)
        {
            using (var db = new BlogContext())
            {
                var query = (from u in db.Users
                             where u.IsActive == true && u.Id == user.Id
                             select u).FirstOrDefault<User>();
                query.IsActive = false;
                db.SaveChanges();
            }
        }

        public List<User> GetUsers()
        {
            using (var db = new BlogContext())
            {
                var query = from u in db.Users.Include(u => u.Roles)
                            where u.IsActive == true
                            select u;
                return query.ToList<User>();
            }
        }

        public User GetUserByUsername(string username)
        {
            using (var db = new BlogContext())
            {
                var query = from u in db.Users.Include(u => u.Roles)
                            where u.IsActive == true && u.Username.Trim() == username.Trim()
                            select u;
                return query.FirstOrDefault();
            }
        }

        public User GetUserById(int userId)
        {
            using (var db = new BlogContext())
            {
                var query = from u in db.Users.Include(u => u.Roles)
                            where u.IsActive == true && u.Id == userId
                            select u;
                return query.FirstOrDefault();
            }
        }

        public List<Tuple<User,int>> GetMostActiveUsers(DateTime fromDate, DateTime toDate)
        {

            using (var db = new BlogContext())
            {

                var lstArticles = (from u in db.Users.Include("Articles").Include("Comments")
                                   select u).ToList<User>();

                List<Tuple<User, int>> lstMostActive = new List<Tuple<User, int>>();

                foreach (User u in lstArticles)
                {
                    int counter = 0;
                    foreach (var article in u.Articles)
                    {
                        if (article.CreationDate >= fromDate && article.CreationDate <= toDate)
                            counter++;

                    }

                    foreach (var comment in u.Comments)
                    {
                        if (comment.CreationDate >= fromDate && comment.CreationDate <= toDate)
                            counter++;
                    }

                    if (counter > 0)
                    {
                        lstMostActive.Add(new Tuple<User, int>(u, counter));
                    }
                }

                return lstMostActive;

            }
        }

        public void UpdateUserComments(int userId, Comment comment)
        {
            using (var db = new BlogContext())
            {
                db.Comments.Attach(comment);
                var query = (from u in db.Users.Include(u => u.Comments)
                             where u.IsActive == true && u.Id == userId
                             select u).FirstOrDefault<User>();

                query.Comments.Add(comment);
                db.SaveChanges();
            }
        }

        public int CountAdmin()
        {
            using (var db = new BlogContext())
            {
                var query = (from u in db.Users.Include(r => r.Roles)
                             where u.IsActive == true
                             select u).ToList();
                int count = 0;
                foreach (var u in query)
                {
                    foreach (var r in u.Roles)
                    {
                        if (r.Type.Equals(RoleType.Administrator))
                            count++;
                    }
                }
                return count;
            }
        }
    }
}
