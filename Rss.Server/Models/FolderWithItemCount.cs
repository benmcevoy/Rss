using System;
using System.Collections.Generic;

namespace Rss.Server.Models
{
    public class FolderWithItemCount
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public DateTime? LastUpdateDateTime { get; set; }

        public Guid FeedId { get; set; }

        public string FeedName { get; set; }

        public Guid? FolderId { get; set; }

        public DateTime? FeedLastUpdateDateTime { get; set; }

        public string FeedUrl { get; set; }

        public string HtmlUrl { get; set; }

        public DateTime? LastBuildDate { get; set; }

        public string UpdatePeriod { get; set; }

        public string UpdateFrequency { get; set; }

        public string FavIcon { get; set; }

        internal Folder Folder { get; set; }

        public ICollection<Item> Items { get; set; }

        public int ItemCount { get; set; }
    }
}