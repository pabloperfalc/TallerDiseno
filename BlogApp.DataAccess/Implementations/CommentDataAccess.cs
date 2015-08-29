using BlogApp.Manager.RequiredInterfaces;
using BlogApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;

namespace BlogApp.DataAccess.Implementations
{
    public class CommentDataAccess : ICommentDataAccess
    {
        public void AddComment(Comment comment) 
        {
            using (var db = new BlogContext())
            {
                comment.ModificationdDate = DateTime.UtcNow;
                comment.CreationDate = DateTime.UtcNow;
                db.Comments.Add(comment);
                db.SaveChanges();
            }
        }

        public void ModifyComment(Comment comment) 
        {
        
        }

        public Comment RetriveComments(Comment comment)
        {
            using (var db = new BlogContext())
            {
                List<Comment> comments = new List<Comment>();
                var dbComment = db.Comments.Include(c => c.Comments).Where(c => c.Id == comment.Id).FirstOrDefault();
                foreach (var item in dbComment.Comments)
                {
                    comments.Add(RetriveComments(item));
                }
                dbComment.Comments = comments;
                return dbComment;
            }
        }

        public List<Comment> GetArticleComments(int articleId)
        {
            using (var db = new BlogContext())
            {
                return db.Comments.Where(c => c.ArticleId == articleId && c.Parent == null).ToList();
            }
        }

    }
}
