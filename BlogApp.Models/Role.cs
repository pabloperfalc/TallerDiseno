﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BlogApp.Models
{
    public class Role
    {
        public int Id { get; set; }

        public string Description { get; set; }

        public List<User> Users { get; set; }

        public RoleType Type { get; set; }
    }
}
