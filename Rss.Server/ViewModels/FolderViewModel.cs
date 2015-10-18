using System;
using System.Collections.Generic;
using System.Linq;

namespace Rss.Server.ViewModels
{
    public class FolderViewModel
    {
        public FolderViewModel()
        {
            Feeds = new List<FeedViewModel>();
        }
    
        public string Id { get; set; }

        public string Name { get; set; }

        public DateTime? LastUpdateDateTime { get; set; }

        public virtual ICollection<FeedViewModel> Feeds { get; set; }

        public int ItemCount { get { return Feeds.Sum(f => f.ItemCount); } }

        public string UnreadClass {
            get { return ItemCount > 0 ? "unread" : "read"; }
        }
    }
}