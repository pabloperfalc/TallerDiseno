using BlogApp.Models;
using BlogApp.Utilities;
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
    }
}
