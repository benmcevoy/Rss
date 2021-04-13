using System;

namespace Rss.Api.V1.Model
{
    public class SubscribePostModel
    {
        public string FeedUrl { get; set; }
        public Guid FolderId { get; set; }
    }
}
