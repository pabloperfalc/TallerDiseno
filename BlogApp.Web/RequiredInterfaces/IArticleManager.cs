using BlogApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BlogApp.Web.RequiredInterfaces
{
    public interface IArticleManager
    {
        List<Article> GetLatest(int count);
        List<Tuple<string, string>> ValidateArticle(Article article);
        void AddArticle(Article article);
        void UpdateArticle(Article article);
        void DeleteArticle(Article article);

    }
}
