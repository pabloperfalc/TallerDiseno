using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Runtime.Serialization.Json;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace BlogApp.VideoPlayer
{
    public partial class MainPage : UserControl
    {
        private List<string> videoList;
        private int index;
        public MainPage()
        {
            InitializeComponent();

            Uri serviceUri = new Uri("/video/getVideoPaths", UriKind.Relative);
            WebClient downloader = new WebClient();
            
            downloader.OpenReadCompleted += downloader_OpenReadCompleted;
            downloader.OpenReadAsync(serviceUri);
        }

        private void player_MediaEnded(object sender, RoutedEventArgs e)
        {
            player.Stop();
            index++;
            player.Source = new Uri(videoList[index]);
            player.Play();
        }

        void downloader_OpenReadCompleted(object sender, OpenReadCompletedEventArgs e)
        {
            var jsonSerializer = new DataContractJsonSerializer(typeof(List<string>));
            videoList = (List<string>)jsonSerializer.ReadObject(e.Result);
            player.Source = new Uri(videoList[0]);
            player.Play();
        }

        //void downloader_DownloadStringCompleted(object sender, DownloadStringCompletedEventArgs e)
        //{

        //    if (e.Error == null)
        //    {
        //        player.Source = new Uri(e.Result);
        //        player.Play();
        //    }
        //}

        private void player_MediaFailed(object sender, ExceptionRoutedEventArgs e)
        {

        }
    }
}
