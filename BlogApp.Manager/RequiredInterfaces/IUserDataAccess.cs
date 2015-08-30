using BlogApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace BlogApp.Manager.RequiredInterfaces
{
    public interface IUserDataAccess
    {
        void AddUser(User user);
        void ModifyUser(User user);
        void RemoveUser(User user);
        List<User> GetUsers();
        User GetUserByUsername(string username);
        User GetUserById(int userId);
        List<Tuple<User, int>> GetMostActiveUsers(DateTime fromDate, DateTime toDate);
        void UpdateUserComments(int userId, Comment comment);
    }
}
