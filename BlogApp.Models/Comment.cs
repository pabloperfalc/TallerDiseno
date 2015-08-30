using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BlogApp.Models
{
    public class Comment
    {
        public int Id { get; set; }

        public string Text { get; set; }
        
        public DateTime CreationDate { get; set; }

        public List<Comment> Comments { get; set; }

        public int? ParentId { get; set; }

        public Comment Parent { get; set; }

        public int ArticleId { get; set; }

        public Article Article { get; set; }

        public bool Read { get; set; }
        
      
    }
}
