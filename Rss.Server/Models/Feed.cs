using System;
using System.Collections.Generic;
    
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

        public string UpdateFrequency { get; set; }

        public string FavIcon { get; set; }
    
        public virtual ICollection<Item> Items { get; set; }
    }
}
