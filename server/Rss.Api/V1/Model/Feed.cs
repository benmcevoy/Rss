using System;

namespace Rss.Api.V1.Model
{
    public class Feed 
    {
        public Guid Id { get; set; }
        public DateTime LastUpdatedDateTime { get; set; }
        public string Name { get; set; }
        public string WebsiteUrl { get; set; }
    }
}