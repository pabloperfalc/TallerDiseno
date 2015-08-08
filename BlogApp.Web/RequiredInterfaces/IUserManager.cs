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
    }
}
