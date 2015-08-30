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
        int ImportArticles(XmlDocument xml);
        List<Tuple<string, string>> ValidateArticle(Article article);
        void AddArticle(Article article);
        void UpdateArticle(Article article, Byte[] ImageBytes);
        Article GetArticleById(int id);
        List<int> GetArticlesPerMonth(int year);
        List<Article> GetPublicArticles(int id);
        void CreateArticle(Article article, Byte[] ImageBytes);

    }
}
