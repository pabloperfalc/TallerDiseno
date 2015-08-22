using BlogApp.Models;
using BlogApp.Web.Models;
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
        public async Task<ActionResult> Register()
        {
            var viewModel = new RegisterUserViewModel
            {
                Title = "Register",
                EditMode = false,
                AdminMode = false,
            };
            
            return View("Register",viewModel);
        }

        [HttpPost]
        public async Task<ActionResult> Register(RegisterUserViewModel userViewModel)
        {
            userViewModel.Title = "Register";
            userViewModel.EditMode = false;
            userViewModel.AdminMode = false;
            //if (userManager.ValidateRegistration(user))
            //{
            //    ModelState.AddModelError(string.Empty, "All fields must be completed");
            //}
            List<Tuple<string, string>> errors = userManager.ValidateUser(userViewModel.User);
            if (errors.Count == 0)
            {
                if (userManager.GetUserByUsername(userViewModel.User.Username) == null)
                {
                    string hashedPassword = userManager.GetHash(userViewModel.User);
                    userViewModel.User.Password = hashedPassword;
                    userManager.AddUser(userViewModel.User);
                    return RedirectToAction("Login");
                }
                else
                {
                    ModelState.AddModelError("Username", "Username already exists");
                    return View(userViewModel);
                }
            }
            else
            {
                foreach (Tuple<string, string> t in errors)
                {
                    ModelState.AddModelError(t.Item1, t.Item2);
                }
                return View(userViewModel);
            }
            //if (!ModelState.IsValid)
            //{
            //    return View(user);
            //}
        }

        [HttpGet]
        public async Task<ActionResult> Edit(int userId)
        {
            var user = userManager.GetUserById(userId);
            var viewModel = new RegisterUserViewModel
                                {
                                    User = user,
                                    IsAdmin = user.Roles.Any(r => r.Description == "Administrator"),
                                    IsBlogger = user.Roles.Any(r => r.Description == "Blogger"),
                                    Title = "Edit",
                                    EditMode = true,
                                    AdminMode =Session["Login"] == null || ((User)Session["Login"]).Roles == null || !((User)Session["Login"]).Roles.Any(role => role.Description == "Administrator"),
                                };
            
            return View("Register",viewModel);
        }

        

        public ActionResult AddUser()
        {
            var viewModel = new RegisterUserViewModel
                                {
                                    Title= "Add New User",
                                    EditMode  = false,
                                    AdminMode = true,
                                };
            
            return View("Register", viewModel);
        }

        [HttpPost]
        [Authorization(Role = "Administrator")]
        public async Task<ActionResult> AddUser(RegisterUserViewModel userViewModel, string returnUrl)
        {


            List<Tuple<string, string>> errors = userManager.ValidateUser(userViewModel.User);
            if (errors.Count == 0)
            {
                if (userManager.GetUserByUsername(userViewModel.User.Username) == null)
                {

                    string hashedPassword = userManager.GetHash(userViewModel.User);
                    userViewModel.User.Password = hashedPassword;

                    userManager.AddUser(userViewModel.User);
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Username already exists");
                    return View(userViewModel.User);
                }
            }
            else
            {
                foreach (Tuple<string, string> t in errors)
                {
                    ModelState.AddModelError(t.Item1, t.Item2);
                }
                return View(userViewModel);
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
