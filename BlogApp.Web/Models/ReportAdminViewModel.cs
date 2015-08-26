﻿using BlogApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace BlogApp.Web.Models
{
    public class ReportAdminViewModel
    {
        public User User { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public DateTime Year { get; set; }
        public List<SelectListItem> Years { get; set; }
    }
}