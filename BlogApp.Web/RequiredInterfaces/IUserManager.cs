using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BlogApp.Models;
namespace BlogApp.Web.RequiredInterfaces
{
    public interface IUserManager
    {
        void AddUser(User user);
        void ModifyUser(User user);
        void RemoveUser(User user);
        bool ValidateLogin(ref User user);
        bool ValidateRegistration(User user);
        bool ValidateEmail(User user);
        string GetHash(User user);
        User GetUserByUsername(string username);
        List<User> GetUsers();
    }
}
