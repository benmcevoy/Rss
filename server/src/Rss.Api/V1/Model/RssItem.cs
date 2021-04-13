using System;
using System.Collections.Generic;

namespace Rss.Api.V1.Model
{
    public class RssItem
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Snippet { get; set; }
        public string Content { get; set; }
        public DateTime PublishedDateTime { get; set; }
        public IList<string> Tags { get; set; } = new List<string>();
        public string Url { get; set; }
        public Guid FeedId { get; set; }
        public DateTime FeedLastUpdatedDateTime { get; set; }
        public string FeedName { get; set; }
        public string FeedWebsiteUrl { get; set; }
        public Guid? FolderId { get; set; }
    }
}

