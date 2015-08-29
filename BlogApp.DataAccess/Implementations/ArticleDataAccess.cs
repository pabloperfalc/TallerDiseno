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

        public List<Article> GetLatest(int count)
        {
            using (var db = new BlogContext())
            {
                var articles = (from a in db.Articles.Include(a => a.Author)
                                select a)
                                .OrderBy(a => a.ModificationdDate)
                                .ThenBy(a => a.CreationDate)
                                .Take(count);

                return articles.ToList();
            }
        }

        public Article GetArticleById(int id)
        {
            using (var db = new BlogContext())
            {
                return (from a in db.Articles.Include(a => a.Author)
                        where a.Id == id
                        select a)
                        .OrderBy(a => a.ModificationdDate)
                        .ThenBy(a => a.CreationDate)
                        .FirstOrDefault();
            }
        }
      
        public List<int> GetArticlesPerMonth(int year)
        {
            using (var db = new BlogContext())
            {

                var allMonths = Enumerable.Range(1, 12);

                var query = db.Articles.Where(a => a.CreationDate.Year == year)
                                        .GroupBy((o => new
                                        {
                                            Month = o.CreationDate.Month,
                                            Year = o.CreationDate.Year
                                        }))
                                        .Select(g => new
                                        {
                                            Month = g.Key.Month,
                                            Year = g.Key.Year,
                                            Total = g.Count()
                                        })
                                        .OrderByDescending(a => a.Year)
                                        .ThenByDescending(a => a.Month)
                                        .ToList();

                var value = (from months in allMonths
                             join date in query on months equals date.Month into d
                             from date in d.DefaultIfEmpty()
                             select new
                             {
                                 Date = months,
                                 Count = (date == null) ? 0 : date.Total
                             })
                                 .OrderBy(a => a.Date)
                                 .ToList().Select(p => p.Count).ToList();


                return value;
            }
        }

        
    }
}
