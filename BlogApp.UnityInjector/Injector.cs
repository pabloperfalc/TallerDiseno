using BlogApp.DataAccess.Implementations;
using BlogApp.ILogger;
using BlogApp.Logger;
using BlogApp.Manager.Implementations;
using BlogApp.Manager.RequiredInterfaces;
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

//Copy dll UnityINjetor al bin del WEB
[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(Injector), "Inject")]

namespace BlogApp.UnityInjector
{
    public class Injector
    {
        public static void Inject()
        {
            var container = new UnityContainer();

            container.RegisterType<IUserManager, UserManager>();
            container.RegisterType<IArticleManager, ArticleManager>();
            container.RegisterType<ICommentManager, CommentManager>();
            container.RegisterType<IRoleManager, RoleManager>();
            container.RegisterType<IUserDataAccess, UserDataAccess>();
            container.RegisterType<IArticleDataAccess, ArticleDataAccess>();
            container.RegisterType<ICommentDataAccess, CommentDataAccess>();
            container.RegisterType<IRoleDataAccess, RoleDataAccess>();
            container.RegisterType<BlogApp.ILogger.ILogger, BlogApp.Logger.Logger>();
            container.RegisterType<ILogDataAccess, LogDataAccess>();

            InjectorResolver.SetResolver(container);
        }
    }
}
