using BlogApp.Manager.RequiredInterfaces;
using BlogApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;

namespace BlogApp.DataAccess.Implementations
{
    public class ArticleDataAccess : IArticleDataAccess
    {
        public void AddArticle(Article article) 
        {
            using (var db = new BlogContext())
            {
                db.Articles.Add(article);
                db.SaveChanges();
            }
        }
        public void ModifyArticle(Article article) 
        {
        
        }
        public void RemoveArticle(Article article) 
        {
        
        }

        public List<Article> GetLatest(int count)
        {
            using (var db = new BlogContext())
            {
                var articles = (from a in db.Articles .Include(a => a.Author)
                                select a)
                                .OrderBy(a => a.ModificationdDate)
                                .ThenBy(a => a.CreationDate)
                                .Take(count);

                return articles.ToList();
            }
        }
    }
}
