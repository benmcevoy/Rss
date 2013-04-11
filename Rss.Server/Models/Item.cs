using System;

namespace Rss.Server.Models
{
    public class Item
    {
        public Guid Id { get; set; }

        public string Raw { get; set; }

        public string Name { get; set; }

        public string Content { get; set; }

        public DateTime? ReadDateTime { get; set; }

        public Guid FeedId { get; set; }

        public string Snippet { get; set; }

        public DateTime? PublishedDateTime { get; set; }

        public string LinkUrl { get; set; }

        internal virtual Feed Feed { get; set; }

        public string FeedName
        {
            get
            {
                return Feed != null ? Feed.Name : "";
            }
        }
    }
}
