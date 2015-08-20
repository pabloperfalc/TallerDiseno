using BlogApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogApp.Web.RequiredInterfaces
{
    public interface IRoleManager
    {
        List<Role> GetRoles();
    }
}
