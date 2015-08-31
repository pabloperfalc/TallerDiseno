using BlogApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BlogApp.Web.RequiredInterfaces
{
    public interface IVideoManager
    {
        void Import(Byte[] VideoBytes, string fileName);
        List<Video> GetVideos();
        void Delete(int Id);
    }
}