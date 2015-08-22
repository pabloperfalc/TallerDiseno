using BlogApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BlogApp.Web.Models
{
    public class RegisterUserViewModel
    {
        public User User { get; set; }

        public bool  IsAdmin { get; set; }

        public bool IsBlogger { get; set; }

        public string Title { get; set; }

        public bool EditMode { get; set; }

        public bool AdminMode { get; set; }

        public string PostAction { get; set; }
    }
}