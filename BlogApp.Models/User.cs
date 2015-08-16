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

        public string SureName { get; set; }

        [StringLength(12, ErrorMessage="Maximo 12 caracteres")]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Use letters only please"), Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }

        public int RoleId { get; set; }

        public Role Role { get; set; }

        public string Email { get; set; }

        public string PicturePath { get; set; }

        public List<Article> Articles { get; set; }

        public bool IsActive { get; set; }
    }
}
