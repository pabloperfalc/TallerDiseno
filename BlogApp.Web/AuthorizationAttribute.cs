using BlogApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BlogApp.Web
{
    public class AuthorizationAttribute:ActionFilterAttribute,IActionFilter
    {
        public RoleType  Role { get; set; }

        void IActionFilter.OnActionExecuting(ActionExecutingContext filterContext)
        {
            HttpSessionStateBase session = filterContext.HttpContext.Session; 
            Controller controller = filterContext.Controller as Controller; 
            if (controller != null)
            {
                if (session["Login"] == null || ((User)session["Login"]).Roles == null || !((User)session["Login"]).Roles.Any(role=>role.Type == Role))
                { 
                    controller.HttpContext.Response.Redirect("./Login");
                }
            }
            base.OnActionExecuting(filterContext); 
        }
    }
}
