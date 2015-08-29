using BlogApp.Manager.RequiredInterfaces;
using BlogApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogApp.DataAccess.Implementations
{
    public class VideoDataAccess : IVideoDataAccess
    {
        public void AddVideo(Video video)
        {
            using (var db = new BlogContext())
            {
                db.Videos.Add(video);
                db.SaveChanges();
            }
        }

        public List<Video> GetVideos()
        {
            using (var db = new BlogContext())
            {
                return db.Videos.ToList();
            }
        }
    }
}
