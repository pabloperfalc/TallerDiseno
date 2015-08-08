﻿using BlogApp.Web.RequiredInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BlogApp.Web.Controllers
{
    public class UserController : Controller
    {
        private IUserManager userManager;
        private BlogApp.ILogger.ILogger logger;

        public UserController(IUserManager userManager, BlogApp.ILogger.ILogger logger)
        {
            
            this.userManager = userManager;
            this.logger = logger;
        }

  
        //
        // GET: /User/

        public ActionResult Index()
        {
            userManager.AddUser("hola");

            logger.Log("hola");
            return View();
        }

    }
}
