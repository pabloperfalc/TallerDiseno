using BlogApp.Models;
using BlogApp.Web.Models;
using BlogApp.Web.RequiredInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;

namespace BlogApp.Web.Controllers
{
    public class ReportController : Controller
    {
        private readonly IUserManager userManager;
        private readonly IArticleManager articleManager;

        public ReportController(IArticleManager articleManager, IUserManager userManager)
        {
            this.userManager = userManager;
            this.articleManager = articleManager;
        }

        [HttpGet]
        public ActionResult Reports()
        {
            return View();
        }

        [HttpGet]
        public ActionResult QueryRankingMostActive(DateTime? fromDate, DateTime? toDate)
        {
            if (fromDate == null || toDate == null)
            {
                return View();
            }
            else
            {

                return View();
            }
        }


        [HttpGet]
        public ActionResult CreateChartByMonthPerYear(int year)
        {
            List<int> lstArticlesCount = articleManager.GetArticlesPerMonth(year);
           

            new Chart(width: 900, height: 300).AddTitle(year.ToString()).AddSeries(chartType: "column", xValue: new[] { "Janury", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December" }, yValues: lstArticlesCount).Write("bmp");
            return null;
            //"10", "20", "30", "30", "30", "30", "30", "30", "30", "30", "30", "30"
        }

        [HttpGet]
        public ActionResult QueryByMonthPerYear(int? year)
        {
            List<SelectListItem> lstYears = new List<SelectListItem>();
            for (int i = 1950; i < 2030; i++)
            {
                lstYears.Add(new SelectListItem { Text = i.ToString(), Value = i.ToString() });
            }
            ViewBag.Years = lstYears;


            if (year != null)
            {

                ViewBag.SelectedYear = year;
            }
            else
            {
                ViewBag.SelectedYear = null;
            }
            ReportAdminViewModel model = new ReportAdminViewModel();
            model.Years = lstYears;
            return View(model);
        }





    }
}
