using BlogApp.Manager.RequiredInterfaces;
using BlogApp.Models;
using BlogApp.Web.RequiredInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BlogApp.Manager.Implementations
{
    public class CommentManager : ICommentManager
    {
        private readonly ICommentDataAccess commentDataAccess;

        public CommentManager(ICommentDataAccess commentDataAccess)
        {
            this.commentDataAccess = commentDataAccess;
        }

        public void AddComment(Models.Comment comment)
        {
            commentDataAccess.AddComment(comment);
        }

        public List<Comment> GetArticleComments(int articleId)
        {
            return commentDataAccess.GetArticleComments(articleId);
        }
    }
}
