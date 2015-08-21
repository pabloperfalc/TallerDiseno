using BlogApp.Models;
using BlogApp.Web.RequiredInterfaces;
using System;
using System.Collections.Generic;
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
        
        public UserController(IUserManager userManager, IRoleManager roleManager, IArticleManager articleManager)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
            this.articleManager = articleManager;
        }

  
        //
        // GET: /User/
        [Authorization(Role = "Administrator")]
        public ActionResult Index()
        {
           
            return View();
        }

        public ActionResult GetVideoPath(int index)
        {
            return Json(new List<string>() { "http://localhost:51295/wildlife.wmv", "http://localhost:51295/wildlife.wmv", "http://localhost:51295/wildlife.wmv" },JsonRequestBehavior.AllowGet);
        }

        public ActionResult Login()
        {
            return View();
        }

        [Authorization(Role = "Administrator")]
        public ActionResult Test()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Login(User user, string returnUrl)
        {
            if (userManager.ValidateLogin(ref user))
            {
                Session["Login"] = user;
                return RedirectToAction("Home");
            }
            else
            {
                if (user.Username == null || user.Password == null)
                {
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
        public async Task<ActionResult> Register()
        {
           // PopulateDropDownList();
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Register(User user)
        {
            //if (userManager.ValidateRegistration(user))
            //{
            //    ModelState.AddModelError(string.Empty, "All fields must be completed");
            //}

            if (!userManager.ValidateEmail(user))
            {
                ModelState.AddModelError("Email", "Check email format. Eg: jack@hello.com");
            }

            if (!ModelState.IsValid)
            {
                return View(user);
            }

                
            if (userManager.GetUserByUsername(user.Username) == null)
            {
                string hashedPassword = userManager.GetHash(user);
                user.Password = hashedPassword;
                userManager.AddUser(user);
                return RedirectToAction("Login");
            }
            else
            {
                ModelState.AddModelError("Username", "Username already exists");
                return View(user);
            }
        }

        public ActionResult AddUser()
        { 
            return View();
        }

        [HttpPost]
        [Authorization(Role = "Administrator")]
        public async Task<ActionResult> AddUser(User user, string returnUrl)
        {
            if (userManager.ValidateRegistration(user))
            {
                if (userManager.ValidateEmail(user))
                {
                    if (userManager.GetUserByUsername(user.Username) == null)
                    {
                        string hashedPassword = userManager.GetHash(user);
                        user.Password = hashedPassword;
                        ///ver como hacer checkbox con MVC 4 para ROLES
                        user.RoleId = 2;
                        /***************************************/
                        userManager.AddUser(user);
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "Username already exists");
                        return View(user);
                    }
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Check email format. Eg: jack@hello.com");
                    return View(user);
                }
            }
            else
            {
                ModelState.AddModelError(string.Empty, "All fields must be completed");
                return View(user);
            }
        }

        public ActionResult ModifyorDelete(string searchString, string returnUrl)
        {
            if (!String.IsNullOrEmpty(searchString))
            {
               User foundUser = userManager.GetUserByUsername(searchString.Trim());
               if (foundUser != null)
               {
                   return View(foundUser);
               }
               else
               {
                   ModelState.AddModelError(string.Empty, "No user found by: "+ searchString);
                   return View(); 
               }
            }
         
            return View();
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

    }
}
