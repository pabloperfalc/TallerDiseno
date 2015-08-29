using BlogApp.ILogger;
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
        private readonly ILogger.ILogger logger;

        public ReportController(IArticleManager articleManager, IUserManager userManager, ILogger.ILogger logger)
        {
            this.userManager = userManager;
            this.articleManager = articleManager;
            this.logger = logger;
        }

        [HttpGet]
        public ActionResult Reports()
        {
            return View();
        }


        public ActionResult QueryLoginSearch(DateTime? fromDate, DateTime? toDate)
        {
            if (fromDate == null || toDate == null)
            {
                return View();
            }
            else
            {
                ReportAdminViewModel model = new ReportAdminViewModel();
                model.FromDate = fromDate.Value;
                model.ToDate = toDate.Value;
                model.LogEntries = logger.GetLog(fromDate.Value, toDate.Value);
                
                return View(model);
            }
        }

        
        public ActionResult QueryRankingMostActive()
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
                ReportAdminViewModel model = new ReportAdminViewModel();
                model.FromDate = fromDate.Value;
                model.ToDate = toDate.Value;
                model.MostActives = userManager.GetMostActiveUsers(fromDate.Value, toDate.Value);
                return View(model);
            }
        }


        [HttpGet]
        public ActionResult CreateChartByMonthPerYear(int year)
        {
            List<int> lstArticlesCount = articleManager.GetArticlesPerMonth(year);
           
            string temp = @"<Chart BackColor=""LightGray"" ForeColor=""LightBlue"">
                      <ChartAreas>
                        <ChartArea Name=""Default"" _Template_=""All"">
                          <AxisY>
                            <LabelStyle Font=""Verdana, 15px"" />
                          </AxisY>
                          <AxisX LineColor=""64, 64, 64, 64"" Interval=""1"">
                            <LabelStyle Font=""Verdana, 14px"" />
                          </AxisX>
                        </ChartArea>
                      </ChartAreas>
                    </Chart>";

            Chart chartPerMonth = new Chart(width: 1000, height: 500, theme: temp).AddTitle(year.ToString()).AddSeries(chartType: "column",xValue: new[] { "Janury", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December" },yValues: lstArticlesCount).Write("bmp");
           
            return null;
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
