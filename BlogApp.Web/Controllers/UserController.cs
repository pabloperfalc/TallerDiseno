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
        private IUserManager userManager;
        
        public UserController(IUserManager userManager)
        {
            this.userManager = userManager;
        }

  
        //
        // GET: /User/

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

        [Authorization(Role="Admin")]
        public ActionResult Test()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Login(User user, string returnUrl)
        {
            if (!ModelState.IsValid)
            {
                return View(user);
            }

            if (userManager.ValidateLogin(ref user))
            {
                Session["Login"] = user;
                return RedirectToAction("Index");
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Usuario o contrasena invalida");
                return View();
            }
        }


    }
}
