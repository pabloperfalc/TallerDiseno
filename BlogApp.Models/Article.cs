using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BlogApp.Models
{
    public class Article
    {
        public int Id { get; set; }

        public string  Name { get; set; }

        public ArticleType Type { get; set; }

        public string Text { get; set; }

        public ArticeLayout Layout { get; set; }

        public string PicturePath { get; set; }

        public DateTime CreationDate { get; set; }

        public DateTime ModificationdDate { get; set; }

        public List<Comment> Comments { get; set; }

        public int AuthorId { get; set; }

        public User Author { get; set; }
    }
}
