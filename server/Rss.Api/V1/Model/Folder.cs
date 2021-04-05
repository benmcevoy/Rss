using System;
using System.Collections.Generic;

namespace Rss.Api.V1.Model
{
    public class Folder
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public IList<Feed> Feeds { get; set; } = new List<Feed>();
    }
}