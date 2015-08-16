using BlogApp.Manager.RequiredInterfaces;
using BlogApp.Models;
using BlogApp.Web.RequiredInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BlogApp.Manager.Implementations
{
    public class UserManager : IUserManager
    {
        private IUserDataAccess userDataAccess;

       public UserManager(IUserDataAccess userDataAccess)
        {
            this.userDataAccess = userDataAccess;
        }

        public void AddUser(User user)
        {
            userDataAccess.AddUser(user);
        }

        public void ModifyUser(User user)
        {
            userDataAccess.ModifyUser(user);
        }

        public void RemoveUser(User user) 
        {
            userDataAccess.RemoveUser(user);
        }

        public bool ValidateLogin(ref User user)
        {
            var existingUser = userDataAccess.GetUserByUsername(user.Username);
            user = existingUser;
            return existingUser != null;
        }
    }
}
