using BlogApp.Models;
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

        [Authorization(Role = RoleType.Administrator)]
        public ActionResult Index()
        {

            return View();
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
                PostAction = "AddEditUser",

            };

            return View("Register", viewModel);
        }

        public ActionResult Edit(int userId)
        {
             var isAdmin =  Session["Login"] != null && ((User)Session["Login"]).Roles != null && ((User)Session["Login"]).Roles.Any(role => role.Type == RoleType.Administrator);

             if (isAdmin || ((User)Session["Login"]).Id == userId)
             {

                 var user = userManager.GetUserById(userId);
                 var adminMode = isAdmin;
                 var postAction = adminMode ? "AddEditUser" : "Register";
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
            foreach (Tuple<string, string> t in errors)
            {
                ModelState.AddModelError("User." + t.Item1, t.Item2);
            }

            if (!ModelState.IsValid)
            {
                return View(userViewModel);
            }

            if (userManager.GetUserByUsername(userViewModel.User.Username) == null)
            {
                AddingUserFields(userViewModel, image);
                List<RoleType> roles = new List<RoleType>();

                roles.Add(RoleType.Blogger);

                userManager.AddUser(userViewModel.User, roles);
                return RedirectToAction("Login");
            }
            else
            {
                ModelState.AddModelError("Username", "Username already exists");
                return View(userViewModel);
            }
        }

        [HttpPost]
        [Authorization(Role = RoleType.Administrator)]
        public ActionResult AddEditUser(RegisterUserViewModel userViewModel, HttpPostedFileBase image)
        {


            List<Tuple<string, string>> errors = userManager.ValidateUser(userViewModel.User);
            if (errors.Count == 0)
            {
                if (userManager.GetUserByUsername(userViewModel.User.Username) == null)
                {

                    AddingUserFields(userViewModel, image);

                    List<RoleType> roles = new List<RoleType>();
                    if (userViewModel.IsAdmin)
                    {
                        roles.Add(RoleType.Administrator);
                    }
                    if (userViewModel.IsAdmin)
                    {
                        roles.Add(RoleType.Blogger);
                    }

                    userManager.AddUser(userViewModel.User, new List<RoleType>());
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

        public ActionResult Home()
        {
            return View(articleManager.GetLatest(10));
        }

        public ActionResult List()
        {
            var users = userManager.GetUsers();
            return View(users);
        }

        private void AddingUserFields(RegisterUserViewModel userViewModel, HttpPostedFileBase image)
        {
            string hashedPassword = userManager.GetHash(userViewModel.User);
            userViewModel.User.Password = hashedPassword;

            if (image != null)
            {
                var imageData = new byte[image.ContentLength];
                image.InputStream.Read(imageData, 0, image.ContentLength);


                string path = Path.Combine(Server.MapPath("~/ProfilePictures"), userViewModel.User.Username + Path.GetExtension(image.FileName));
                if (System.IO.File.Exists(path))
                {
                    System.IO.File.Delete(path);
                }
                image.SaveAs(path);

                userViewModel.User.PicturePath = "~/ProfilePictures/" + userViewModel.User.Username + Path.GetExtension(image.FileName);
            }
        }

    }
}
