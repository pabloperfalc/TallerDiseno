using BlogApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BlogApp.Web.RequiredInterfaces
{
    public interface IVideoManager
    {
        void AddVideo(Video video);
        void Import(Byte[] VideoBytes);
        List<Video> GetVideos();
    }
}