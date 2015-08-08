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

        public DateTime ModificationdDate { get; set; }

        public List<Comment> Comments { get; set; }
    }
}
