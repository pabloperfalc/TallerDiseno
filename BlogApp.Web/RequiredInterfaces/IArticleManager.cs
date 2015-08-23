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
<<<<<<< HEAD
        List<Article> Cargar(XmlDocument xml);
=======
        List<Tuple<string, string>> ValidateArticle(Article article);
        void AddArticle(Article article);
        void UpdateArticle(Article article);
        void DeleteArticle(Article article);

>>>>>>> 74bd980db486fc924ec24ae1d7b137715d06dd99
    }
}
