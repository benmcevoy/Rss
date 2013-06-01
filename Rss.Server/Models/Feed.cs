using System;
using System.Collections.Generic;
using System.Linq;

namespace Rss.Server.Models
{

    public class Feed
    {
        public Feed()
        {
            Items = new HashSet<Item>();
        }

        public Guid Id { get; set; }

        public string Name { get; set; }

        public Guid? FolderId { get; set; }

        public DateTime? LastUpdateDateTime { get; set; }

        public string FeedUrl { get; set; }

        public string HtmlUrl { get; set; }

        public DateTime? LastBuildDate { get; set; }

        public string UpdatePeriod { get; set; }

        public int UpdateFrequency { get; set; }

        public string FavIcon { get; set; }

        internal virtual Folder Folder { get; set; }

        public virtual ICollection<Item> Items { get; set; }

        public string FolderName
        {
            get
            {
                return Folder != null ? Folder.Name : "";
            }
        }

        public int ItemCount { get; set; }
    }
}

