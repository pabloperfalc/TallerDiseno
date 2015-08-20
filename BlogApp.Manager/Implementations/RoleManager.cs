using BlogApp.Manager.RequiredInterfaces;
using BlogApp.Models;
using BlogApp.Web.RequiredInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogApp.Manager.Implementations
{
    public class RoleManager : IRoleManager
    {
        private IRoleDataAccess roleDataAccess;

        public RoleManager(IRoleDataAccess roleDataAccess)
        {
            this.roleDataAccess = roleDataAccess;
        }

        public List<Role> GetRoles()
        {
            return roleDataAccess.GetRoles();
        }
    }
}
