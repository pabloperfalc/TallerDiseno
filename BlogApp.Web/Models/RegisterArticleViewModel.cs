using BlogApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BlogApp.Web.Models
{
    public class RegisterArticleViewModel
    {
        public Article Article { get; set; }

        public string Title { get; set; }

        public bool EditMode { get; set; }

        public string PostAction { get; set; }

        public bool IsPublic { get; set; }
        public bool IsPrivate { get; set; }

    }
}