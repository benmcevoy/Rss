using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Rss.Api.Data
{
    public class Feed
    {
        public bool IsInFolder => FolderId != null;

        [Key]
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Guid? FolderId { get; set; }
        public Folder Folder { get; set; }
        public DateTime LastUpdateDateTime { get; set; }
        public string FeedUrl { get; set; }
        public string HtmlUrl { get; set; }
        public string UpdatePeriod { get; set; }
        public int UpdateFrequency { get; set; }
        public string FavIcon { get; set; }
        public List<Item> Items { get; set; } = new List<Item>();
    }
}