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
                comment.CreationDate = DateTime.UtcNow;
                db.Comments.Add(comment);
                db.SaveChanges();
            }
        }

        public Comment RetriveComments(Comment comment)
        {
            using (var db = new BlogContext())
            {
                List<Comment> comments = new List<Comment>();
                var dbComment = db.Comments.Include(x => x.Author).Include(c => c.Comments).Where(c => c.Id == comment.Id).FirstOrDefault();
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
                var query = (from c in db.Comments.Include(x => x.Author)
                            where (c.ArticleId == articleId && c.Parent == null)
                            select c).ToList();
                return query;
            }
        }

        public List<Comment> GetUnreadComments(int userId)
        {
            using (var db = new BlogContext())
            {
                return GetUnreadCommenstQuery(db,userId).ToList();
            }
        }

        public int GetUnreadCommentsCount(int userId)
        {
            using (var db = new BlogContext())
            {
                return GetUnreadCommenstQuery(db, userId).Count();
            }
        }

        private IQueryable<Comment> GetUnreadCommenstQuery(BlogContext db,int userId)
        { 
            return 
                    from commnet in db.Comments.Include(c => c.Article).Include(x => x.Author)
                    where commnet.Article.AuthorId == userId
                    where commnet.AuthorId != userId
                    where !commnet.ParentId.HasValue
                    where !commnet.Read
                    select commnet;
        }

        public void MarkAsRead(int commentId)
        {
            using (var db = new BlogContext())
            {
                var comment = db.Comments.Find(commentId);
                comment.Read = true;
                db.SaveChanges();
            }
        }
    }
}
