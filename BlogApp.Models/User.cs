using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace BlogApp.Models
{
    public class User
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Surname { get; set; }

        [StringLength(12, ErrorMessage="Maximun lenght is 12 characters")]
        [RegularExpression(@"^[a-zA-Z][a-zA-Z0-9]*$", ErrorMessage = "Use letters and numbers only, please")]
        public string Username { get; set; }

        public string Password { get; set; }

        public List<Role> Roles { get; set; }

        public string Email { get; set; }

        public string PicturePath { get; set; }

        public List<Article> Articles { get; set; }

        public bool IsActive { get; set; }
    }
}
