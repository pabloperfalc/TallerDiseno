using BlogApp.Manager.Implementations;
using BlogApp.Web.RequiredInterfaces;
using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogApp.UnityInjector
{
    public class Injector
    {
        static Injector()
        {
            var container = new UnityContainer();

            container.RegisterType<IUserManager, UserManager>();
        }
    }
}
