using BlogApp.Manager.RequiredInterfaces;
using BlogApp.Models;
using BlogApp.Web.RequiredInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BlogApp.Manager.Implementations
{
    public class ArticleManager : IArticleManager
    {

        private readonly IArticleDataAccess articleDataAccess;
        public ArticleManager(IArticleDataAccess articleDataAccess)
        {
            this.articleDataAccess = articleDataAccess;
        }

        public List<Article> GetLatest(int count)
        {
            return articleDataAccess.GetLatest(count);
        }
    }
}
