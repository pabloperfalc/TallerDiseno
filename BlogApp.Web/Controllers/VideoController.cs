using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BlogApp.Web.Controllers
{
    public class VideoController : Controller
    {
        public ActionResult GetVideoPaths()
        {
            return Json(new List<string>() 
                    { 
                        "http://localhost:51295/wildlife.wmv", 
                        "http://localhost:51295/wildlife.wmv", 
                        "http://localhost:51295/wildlife.wmv" 
                    }, JsonRequestBehavior.AllowGet);
        }

    }
}
