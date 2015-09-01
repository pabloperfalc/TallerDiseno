using BlogApp.Models;
using BlogApp.Web.RequiredInterfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace BlogApp.Web.Controllers
{
    public class VideoController : Controller
    {
        private readonly IVideoManager videoManager;

        public VideoController(IVideoManager videoManager)
        {
            this.videoManager = videoManager;
        }

        public ActionResult GetVideoPaths()
        {
            try
            {
                var uri = Request.Url;
                var host = uri.GetLeftPart(UriPartial.Authority);
                var videos = videoManager.GetVideos().Select(v => host + v.Path).ToList<string>();
                return Json(videos, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                ViewBag.ErrorTitle = "Server Error";
                ViewBag.ErrorDescription = "Please try again later";
                return View("~/Views/Shared/ErrorPage.cshtml");
            }
        }
       

        [HttpGet]
        [Authorization(Roles = new[] { RoleType.Administrator })]
        public ActionResult ImportVideos()
        {
            return View(-1);
        }

        [HttpPost]
        [Authorization(Roles = new[] { RoleType.Administrator })]
        public async Task<ActionResult> ImportVideos(HttpPostedFileBase file)
        {
            try
            {
                if (file != null)
                {
                    var videoData = new byte[file.ContentLength];
                    file.InputStream.Read(videoData, 0, file.ContentLength);
                    videoManager.Import(videoData, file.FileName);
                    return View(0);
                }
                return View(0);
            }
            catch (Exception e)
            {
                ViewBag.ErrorTitle = "Server Error";
                ViewBag.ErrorDescription = "Please try again later";
                return View("~/Views/Shared/ErrorPage.cshtml");
            }
        }

        [HttpGet]
        [Authorization(Roles = new[] { RoleType.Administrator })]
        public ActionResult List()
        {
            try
            {
                return View(videoManager.GetVideos());
            }
            catch (Exception e)
            {
                ViewBag.ErrorTitle = "Server Error";
                ViewBag.ErrorDescription = "Please try again later";
                return View("~/Views/Shared/ErrorPage.cshtml"); 
            }
        }

        [HttpGet]
        [Authorization(Roles = new[] { RoleType.Administrator })]
        public async Task<ActionResult> Delete(int Id)
        {
            try
            {
                videoManager.Delete(Id);
                return View("List", videoManager.GetVideos());
            }
            catch (Exception e)
            {
                ViewBag.ErrorTitle = "Server Error";
                ViewBag.ErrorDescription = "Please try again later";
                return View("~/Views/Shared/ErrorPage.cshtml");
            }
        }
    }
}
