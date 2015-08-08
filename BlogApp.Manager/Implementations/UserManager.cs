using BlogApp.Manager.RequiredInterfaces;
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

        public void AddUser(string b)
        {
            userDataAccess.AddUser(b);
        }
    }
}
