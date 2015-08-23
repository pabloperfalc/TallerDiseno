using BlogApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogApp.Manager.RequiredInterfaces
{
    public interface IRoleDataAccess
    {
        void AddRole(Role role);
        void ModifyRole(Role user);
        void RemoveRole(Role user);
        List<Role> GetRoles();
        Role GetRoleByType(RoleType type);
        
    }
}
