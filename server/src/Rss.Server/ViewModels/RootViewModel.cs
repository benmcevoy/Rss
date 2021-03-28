using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Rss.Server.ViewModels
{
    public class RootViewModel
    {
        public RootViewModel()
        {
            Name = "home";
            Folders = new List<FolderViewModel>();
            Feeds = new List<FeedViewModel>();
        }

        public string Name { get; set; }

        public IEnumerable<FolderViewModel> Folders { get; set; }

        public IEnumerable<FeedViewModel> Feeds { get; set; }
    }
}