using BlogApp.Web.RequiredInterfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using BlogApp.Models;
using System.Xml;
using System.IO;
using BlogApp.Web.Models;

namespace BlogApp.Web.Controllers
{
    public class ArticleController : Controller
    {

        private readonly IArticleManager articleManager;
        private readonly ICommentManager commentManager;

        public ArticleController(IArticleManager articleManager, ICommentManager commentManager)
        {
            this.articleManager = articleManager;
            this.commentManager = commentManager;
        }

        [HttpGet]
        public ActionResult EditArticle(int articleId)
        {
            var viewModel = new RegisterArticleViewModel
            {
                Article = articleManager.GetArticleById(articleId),
                Title = "Edit Article",
                EditMode = true,
                PostAction = "EditArticle",

            };
            return View("CreateArticle", viewModel);
        }

        //[Authorization(Role = RoleType.Blogger)]
        public ActionResult CreateArticle()
        {
            var viewModel = new RegisterArticleViewModel
            {
                Title = "Create Article",
                EditMode = false,
                PostAction = "CreateArticle",

            };
            return View("CreateArticle", viewModel);
        }

        [HttpPost]
        public async Task<ActionResult> EditArticle(Article article, HttpPostedFileBase image)
        {
            List<Tuple<string, string>> errors = articleManager.ValidateArticle(article);
            if (errors.Count == 0)
            {
                article.ModificationdDate = DateTime.Now;
                article.CreationDate = DateTime.Now;
                //article.Layout = ViewBag.Layout;
                //article.Type = ViewBag.Type;
                if (image != null)
                    {
                        var imageData = new byte[image.ContentLength];
                        image.InputStream.Read(imageData, 0, image.ContentLength);
                        articleManager.UpdateArticle(article,imageData);
                    }
                    else
                    {
                        articleManager.UpdateArticle(article,null);
                    }
                return RedirectToAction("ArticleView", new { id = article.Id });
            }
            else
            {
                foreach (Tuple<string, string> t in errors)
                {
                    ModelState.AddModelError(t.Item1, t.Item2);
                }
                return View(article);
            }
        }

        [HttpPost]
        //[Authorization(Role = RoleType.Blogger)]
        public async Task<ActionResult> CreateArticle(Article article, HttpPostedFileBase image)
        {
            if (Request.Form["Confirm"] != null)
            {
                // Code for function 1

                List<Tuple<string, string>> errors = articleManager.ValidateArticle(article);
                if (errors.Count == 0)
                {
                    //var user = new User();
                    //user = (User)Session["Login"];
                    //article.Author = user;
                    //article.AuthorId = user.Id;
                    article.AuthorId = 1;

                    article.ModificationdDate = DateTime.Now;
                    article.CreationDate = DateTime.Now;
                    article.Layout = ViewBag.Layout;
                    //article.Type = ViewBag.Type;

                    if (image != null)
                    {
                        var imageData = new byte[image.ContentLength];
                        image.InputStream.Read(imageData, 0, image.ContentLength);
                        articleManager.CreateArticle(article, imageData);
                    }
                    else
                    {
                        articleManager.CreateArticle(article, null);
                    }
                    return RedirectToAction("ArticleView", new { id = article.Id });
                }
                else
                {
                    foreach (Tuple<string, string> t in errors)
                    {
                        ModelState.AddModelError(t.Item1, t.Item2);
                    }
                    return View(article);
                }
            }
            else
            {
                if (Request.Form["Import"] != null)
                {
                    return RedirectToAction("Import");
                }
                return View();
            }
        }

        [Authorization(Role = RoleType.Blogger)]
        public ActionResult Import()
        {
            return View(-1);
        }

        [HttpPost]
        [Authorization(Role = RoleType.Blogger)]
        public async Task<ActionResult> Import(HttpPostedFileBase file)
        {
            if (file != null)
            {
                string fileName = Path.GetFileName(file.FileName);
                string path = Path.Combine(Server.MapPath("~/App_Data"), fileName);
                file.SaveAs(path);
                var doc = new XmlDocument();
                doc.Load(Server.MapPath("~/App_Data/" + fileName));
                int errors = articleManager.ImportArticles(doc);
                return View(errors);
            }
            return View(0);
        }

        //[Authorization(Role = RoleType.Blogger)]
        public ActionResult ArticleView(int id)
        {
            var article = articleManager.GetArticleById(id);
            if (article == null || article.Type == ArticleType.Private)
            {
                ViewBag.ErrorTitle = "No article was found!";
                ViewBag.ErrorDescription = "";
                return View("~/Views/Shared/ErrorPage.cshtml");
            }
            else
            {
                return View(article);
            }
        }

        [HttpPost]
        [Authorization(Role = RoleType.Blogger)]
        public ActionResult AddComment(Comment comment)
        {
            var user = (User)Session["Login"];

            var newComment = new Comment();
            newComment.Text = comment.Text;
            newComment.ArticleId = comment.ArticleId;
            newComment.ParentId = comment.ParentId;
            commentManager.AddComment(newComment);

            return RedirectToAction("ArticleView", new { id = comment.ArticleId });
        }

        [HttpPost]
        [Authorization(Role = RoleType.Blogger)]
        public ActionResult CommentArticle(Comment comment)
        {
            var user = (User)Session["Login"];

           
            commentManager.AddComment(comment);

            return RedirectToAction("ArticleView", new { id = comment.ArticleId });
        }

        [HttpGet]
        //[Authorization(Role = RoleType.Blogger)]
        public ActionResult List(int id)
        {
            List<Article> lstPublicArticles = articleManager.GetPublicArticles(id);
            return View(lstPublicArticles);

        }


    }
}
