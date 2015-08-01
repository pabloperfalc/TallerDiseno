using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BlogApp.Web
{
    public class InjectorResolver
    {
        public static void SetResolver(UnityContainer container)
        {
            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }
    }
}