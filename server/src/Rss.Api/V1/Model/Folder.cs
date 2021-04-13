using System;
using System.Collections.Generic;
using System.Linq;

namespace Rss.Api.V1.Model
{
    public class Folder
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public IList<Feed> Feeds { get; set; } = new List<Feed>();
        public int Count => Feeds.Sum(f => f.Count);
    }
}