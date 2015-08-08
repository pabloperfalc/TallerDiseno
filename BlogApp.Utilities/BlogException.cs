using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace BlogApp.Utilities
{
    public class BlogException : Exception
    {
         public BlogException()
            : base()
        {
        }

         public BlogException(string message)
            : base(message)
        {
        }
    }
}
