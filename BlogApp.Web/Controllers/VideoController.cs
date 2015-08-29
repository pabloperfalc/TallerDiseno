﻿using BlogApp.Web.RequiredInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
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
            return Json(new List<string>() 
                    { 
                        "http://localhost:51295/wildlife.wmv", 
                        "http://localhost:51295/wildlife.wmv", 
                        "http://localhost:51295/wildlife.wmv" 
                    }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ImportVideos()
        {

            return View();
        }

    }
}
