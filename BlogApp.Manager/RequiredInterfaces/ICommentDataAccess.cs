using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BlogApp.Models;


namespace BlogApp.Manager.RequiredInterfaces
{
    public interface ICommentDataAccess
    {
        void AddComment(Comment comment);

        Comment RetriveComments(Comment comment);

        List<Comment> GetArticleComments(int articleId);
    }
}
