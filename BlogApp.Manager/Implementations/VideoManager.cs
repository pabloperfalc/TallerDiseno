using BlogApp.Manager.RequiredInterfaces;
using BlogApp.Models;
using BlogApp.Web.RequiredInterfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogApp.Manager.Implementations
{
    public class VideoManager : IVideoManager
    {
        private readonly IVideoDataAccess videoDataAccess;

        public VideoManager(IVideoDataAccess videoDataAccess)
        {
            this.videoDataAccess = videoDataAccess;
        }
        public void AddVideo(Video video)
        {
            
            videoDataAccess.AddVideo(video);
        }

        public void Import(Byte[] VideoBytes)
        {
            string name = Guid.NewGuid().ToString() + ".wmv";
            string path = System.AppDomain.CurrentDomain.BaseDirectory + "VideoAds/" + name;
            File.WriteAllBytes(path, VideoBytes);
            path = "/VideoAds/" + name;
            Video video = new Video();
            video.Path = path;

            this.AddVideo(video);

        }
    }
}
