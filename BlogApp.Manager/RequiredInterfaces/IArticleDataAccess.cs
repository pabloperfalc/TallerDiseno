﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BlogApp.Models;

namespace BlogApp.Manager.RequiredInterfaces
{
    public interface IArticleDataAccess
    {
        void AddArticle(Article article);
        void ModifyArticle(Article article);
        void RemoveArticle(Article article);
        List<Article> GetLatest(int count);
        Article GetArticleById(int id);
    }
}
