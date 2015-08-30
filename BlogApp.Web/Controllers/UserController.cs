﻿using BlogApp.Models;
using BlogApp.Web.Models;
using BlogApp.Web.RequiredInterfaces;
using BlogApp.ILogger;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace BlogApp.Web.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserManager userManager;
        private readonly IRoleManager roleManager;
        private readonly IArticleManager articleManager;
        private readonly ILogger.ILogger logger;
        

        public UserController(IUserManager userManager, IRoleManager roleManager, IArticleManager articleManager, ILogger.ILogger logger)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
            this.articleManager = articleManager;
            this.logger = logger;
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(User user, string returnUrl)
        {
            if (userManager.ValidateLogin(ref user))
            {
                logger.Log("", LogType.Login, user.Username);

                Session["Login"] = user;
                return RedirectToAction("Home");
            }
            else
            {
                if (user.Username == null || user.Password == null)
                {
                    ModelState.AddModelError(string.Empty, "All fields are required");
                    return View();
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Username or password is invalid");
                    return View();
                }
            }
        }

        [HttpGet]
        public ActionResult Register()
        {
            var viewModel = new RegisterUserViewModel
            {
                Title = "Register",
                EditMode = false,
                AdminMode = false,
                PostAction = "Register",
            };

            return View("Register", viewModel);
        }

        [HttpGet]
        public ActionResult AddUser()
        {
            var viewModel = new RegisterUserViewModel
            {
                Title = "Add New User",
                EditMode = false,
                AdminMode = true,
                PostAction = "AddUser",
            };

            return View("Register", viewModel);
        }

        public ActionResult Edit(int Id)
        {
             var isAdmin =  Session["Login"] != null && ((User)Session["Login"]).Roles != null && ((User)Session["Login"]).Roles.Any(role => role.Type == RoleType.Administrator);

             if (isAdmin || ((User)Session["Login"]).Id == Id)
             {

                 var user = userManager.GetUserById(Id);
                 var adminMode = isAdmin;
                 var postAction = "EditUser";
                 var viewModel = new RegisterUserViewModel
                 {
                     User = user,
                     IsAdmin = user.Roles.Any(r => r.Type == RoleType.Administrator),
                     IsBlogger = user.Roles.Any(r => r.Type == RoleType.Blogger),
                     Title = "Edit",
                     EditMode = true,
                     AdminMode = adminMode,
                     PostAction = postAction
                 };
                 return View("Register", viewModel);
             }
             return RedirectToAction("Login");
        }

        [HttpPost]
        public ActionResult Register(RegisterUserViewModel userViewModel, HttpPostedFileBase image)
        {
            List<Tuple<string, string>> errors = userManager.ValidateUser(userViewModel.User);
            if (errors.Count != 0)
            {
                foreach (Tuple<string, string> t in errors)
                {
                    ModelState.AddModelError("User." + t.Item1, t.Item2);
                }
                return View(userViewModel);
            }

            if (userManager.GetUserByUsername(userViewModel.User.Username) == null)
            {
                string hashedPassword = userManager.GetHash(userViewModel.User);
                userViewModel.User.Password = hashedPassword;

                List<RoleType> roles = new List<RoleType>();
                roles.Add(RoleType.Blogger);

                if (image != null)
                {
                    var imageData = new byte[image.ContentLength];
                    image.InputStream.Read(imageData, 0, image.ContentLength);
                    userManager.AddUser(userViewModel.User, roles, imageData);
                }
                else 
                {
                    userManager.AddUser(userViewModel.User, roles, null);                
                }            
                return RedirectToAction("Login");
            }
            else
            {
                ModelState.AddModelError("User.Username", "The Username already exsists");
                return View(userViewModel);
            }
        }


        [HttpPost]
        [Authorization(Roles = new[] { RoleType.Administrator })]
        public ActionResult AddUser(RegisterUserViewModel userViewModel, HttpPostedFileBase image)
        { 
             List<Tuple<string, string>> errors = userManager.ValidateUser(userViewModel.User);
             if (errors.Count == 0)
             {
                 if (!userViewModel.IsAdmin && !userViewModel.IsBlogger)
                 {
                     ModelState.AddModelError("", "At least one checkbox must be selected");
                     return View("Register", userViewModel);
                 }

                 if (userManager.GetUserByUsername(userViewModel.User.Username) == null)
                 {
                     List<RoleType> roles = new List<RoleType>();

                     roles = new List<RoleType>();
                     if (userViewModel.IsAdmin)
                     {
                         roles.Add(RoleType.Administrator);
                     }
                     if (userViewModel.IsBlogger)
                     {
                         roles.Add(RoleType.Blogger);
                     }

                     string hashedPassword = userManager.GetHash(userViewModel.User);
                     userViewModel.User.Password = hashedPassword;

                     roles.Add(RoleType.Blogger);

                     if (image != null)
                     {
                         var imageData = new byte[image.ContentLength];
                         image.InputStream.Read(imageData, 0, image.ContentLength);
                         userManager.AddUser(userViewModel.User, roles, imageData);
                     }
                     else
                     {
                         userManager.AddUser(userViewModel.User, roles, null);
                     }
                     return RedirectToAction("Home");
                 }
                 else
                 {
                     ModelState.AddModelError("User.Username", "The Username already exsists");
                     return View(userViewModel);
                 }

             }
             else
             {
                 foreach (Tuple<string, string> t in errors)
                 {
                     ModelState.AddModelError("User." + t.Item1, t.Item2);
                 }
                 return View("Register", userViewModel);
             }
        }


        [HttpPost]

        [Authorization(Roles = new [] { RoleType.Administrator, RoleType.Blogger })]
        public ActionResult EditUser(RegisterUserViewModel userViewModel, HttpPostedFileBase image)
        {
            List<Tuple<string, string>> errors = userManager.ValidateUser(userViewModel.User);
            if (errors.Count == 0)
            {
                List<RoleType> roles = new List<RoleType>();
                var isAdmin = Session["Login"] != null && ((User)Session["Login"]).Roles != null && ((User)Session["Login"]).Roles.Any(role => role.Type == RoleType.Administrator);

                if (isAdmin)
                {
                    if (!userViewModel.IsAdmin && !userViewModel.IsBlogger)
                    {
                        ModelState.AddModelError("", "At least one checkbox must be selected");
                        return View("Register", userViewModel);
                    }

                    roles = new List<RoleType>();
                    if (userViewModel.IsAdmin)
                    {
                        roles.Add(RoleType.Administrator);
                    }
                    if (userViewModel.IsBlogger)
                    {
                        roles.Add(RoleType.Blogger);
                    }
                }
                else 
                {
                    if(((User)Session["Login"]).Id != userViewModel.User.Id)
                    {
                        //Tirar error;
                    }

                    roles.AddRange(((User)Session["Login"]).Roles.Select(r => r.Type));
                }

                Edit(userViewModel, image, roles);
                
                return RedirectToAction("Home");
            }
            else
            {
                foreach (Tuple<string, string> t in errors)
                {
                    ModelState.AddModelError("User." + t.Item1, t.Item2);
                }
                return View("Register", userViewModel);
            }
        }

        private void Edit(RegisterUserViewModel userViewModel, HttpPostedFileBase image, List<RoleType> roles)
        {
            string hashedPassword = userManager.GetHash(userViewModel.User);
            userViewModel.User.Password = hashedPassword;

            if (image != null)
            {
                var imageData = new byte[image.ContentLength];
                image.InputStream.Read(imageData, 0, image.ContentLength);
                userManager.ModifyUser(userViewModel.User, roles, imageData);
            }
            else
            {
                userManager.ModifyUser(userViewModel.User, roles, null);
            }
        }

        public ActionResult Home()
        {
            return View(articleManager.GetLatest(10));
        }

        public ActionResult List()
        {
            var users = userManager.GetUsers();
            return View(users);
        }

        public ActionResult LogOut()
        {
            Session["Login"] = null;
            return RedirectToAction("Login");
        }

        public ActionResult GetNotificationCount()
        {
            Random rnd = new Random();
            int num = rnd.Next(1, 20); 
            return Json(num, JsonRequestBehavior.AllowGet);
        }
    }
}
