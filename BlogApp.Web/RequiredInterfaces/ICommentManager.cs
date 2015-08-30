using BlogApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BlogApp.Web.RequiredInterfaces
{
    public interface ICommentManager
    {
        void AddComment(Comment comment);

        List<Comment> GetArticleComments(int articleId);

        List<Comment> GetUnreadComments(int userId);

        int GetUnreadCommentsCount(int userId);
    }
}
