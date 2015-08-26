﻿using BlogApp.Web.RequiredInterfaces;
using System;
using System.Collections.Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using BlogApp.Models;
using System.Xml;
using System.IO;

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

        //
        // GET: /Article/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult CreateArticle()
        {

            return View();
        }

        [HttpPost]
        // [Authorization(Role = "Blogger")]
        public async Task<ActionResult> CreateArticle(Article article, HttpPostedFileBase image)
        {
            if (Request.Form["Confirm"] != null)
            {
                // Code for function 1

                List<Tuple<string, string>> errors = articleManager.ValidateArticle(article);
                if (errors.Count == 0)
                {
                    var imageData = new byte[image.ContentLength];
                    image.InputStream.Read(imageData, 0, image.ContentLength);

                    article.AuthorId = 1;
                    string name = Guid.NewGuid().ToString();
                    string path = Path.Combine(Server.MapPath("~/ArticlePictures"), name + Path.GetExtension(image.FileName));
                    if (System.IO.File.Exists(path))
                    {
                        System.IO.File.Delete(path);
                    }
                    image.SaveAs(path);
                    
                    //var user = new User();
                    //user = (User)Session["Login"];
                    //article.Author = user;
                    //article.AuthorId = user.Id;
                    article.PicturePath = "~/ArticlePictures/" + name + Path.GetExtension(image.FileName);
                    article.ModificationdDate = DateTime.Now;
                    article.CreationDate = DateTime.Now;
                    article.Layout = ViewBag.Layout;
                    article.Type = ViewBag.Type;
                    articleManager.AddArticle(article);
                    return RedirectToAction("Home"); //cambiar esto
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

        public ActionResult Import()
        {
            return View(-1);
        }

        [HttpPost]
        public async Task<ActionResult> Import(HttpPostedFileBase file)
        {
            if (file !=null)
            {
                string fileName = Path.GetFileName(file.FileName);
                string path = Path.Combine(Server.MapPath("~/App_Data"), fileName);
                file.SaveAs(path);
                var doc = new XmlDocument();
                doc.Load(Server.MapPath("~/App_Data/"+fileName));
                int errors = articleManager.ImportArticles(doc);
                return View(errors);
            }
            return View(0);
        }

        public ActionResult ArticleView(int id)
        {
            var article = articleManager.GetArticleById(id);
            
            return View(article);
        }

        [HttpPost]
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

    }
}
