using System;

namespace Rss.Api.V2.Model
{
    public class SubscribePostModel
    {
        public string FeedUrl { get; set; }
        public Guid FolderId { get; set; }
    }
}
