using BlogApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace BlogApp.Web.RequiredInterfaces
{
    public interface IArticleManager
    {
        List<Article> GetLatest(int count);
        List<Article> Cargar(XmlDocument xml);
    }
}
