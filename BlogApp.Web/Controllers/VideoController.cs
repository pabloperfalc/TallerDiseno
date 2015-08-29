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
            var uri = Request.Url;
            var host = uri.GetLeftPart(UriPartial.Authority);
            var videos = videoManager.GetVideos().Select(v => host + v.Path).ToList<string>();
            return Json(videos, JsonRequestBehavior.AllowGet);
        }
       

        [HttpGet]
        public ActionResult ImportVideos()
        {
            return View(-1);
        }

        [HttpPost]
        public async Task<ActionResult> ImportVideos(HttpPostedFileBase file)
        {
            if (file !=null)
            {
                var videoData = new byte[file.ContentLength];
                file.InputStream.Read(videoData, 0, file.ContentLength);
                videoManager.Import(videoData);
                return View(0);
            }
            return View(0);
        }

    }
}
