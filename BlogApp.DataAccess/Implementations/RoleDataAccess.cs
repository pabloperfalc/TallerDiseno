using BlogApp.Manager.RequiredInterfaces;
using BlogApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogApp.DataAccess.Implementations
{
    public class RoleDataAccess : IRoleDataAccess
    {
        public List<Role> GetRoles()
        {
            using (var db = new BlogContext())
            {
                var roles = (from p in db.Roles select p);
                return roles.ToList();
            }
        }

        public Role GetRoleByType(RoleType type)
        {
            using (var db = new BlogContext())
            {
                return (from p in db.Roles 
                where p.Type == type 
                select p).FirstOrDefault();
            }
        }
    }
}
