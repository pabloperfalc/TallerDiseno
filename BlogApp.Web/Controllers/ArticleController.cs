using BlogApp.Web.RequiredInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BlogApp.Web.Controllers
{
    public class ArticleController : Controller
    {

        private IArticleManager articleManager;

        public ArticleController(IArticleManager articleManager)
        {
            this.articleManager = articleManager;
        }

        //
        // GET: /Article/

        public ActionResult Index()
        {
            return View();
        }

    }
}
