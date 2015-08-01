using BlogApp.Manager.Implementations;
using BlogApp.UnityInjector;
using BlogApp.Web;
using BlogApp.Web.Controllers;
using BlogApp.Web.RequiredInterfaces;
using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//Copiar dll UnityINjetor al bin del WEB
[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(Injector), "Inject")]

namespace BlogApp.UnityInjector
{
    public class Injector
    {
        public static void Inject()
        {
            var container = new UnityContainer();

            container.RegisterType<IUserManager, UserManager>();

            InjectorResolver.SetResolver(container);
        }
    }
}
