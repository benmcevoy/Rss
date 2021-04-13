using System;
using System.Collections.Generic;

namespace Rss.Api.V1.Model
{
    public class RssItemList
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public Guid? Id { get; set; }
        public List<RssItem> RssItems { get; set; } = new List<RssItem>();
    }
}
