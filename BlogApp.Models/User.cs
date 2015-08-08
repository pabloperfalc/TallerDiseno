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
        public string Username { get; set; }

        public string Passwod { get; set; }

        public int RoleId { get; set; }

        public Role Role { get; set; }

        public string Email { get; set; }

        public string PicturePath { get; set; }

        public List<Article> Articles { get; set; }

    }
}
