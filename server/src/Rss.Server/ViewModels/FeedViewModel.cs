using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Rss.Server.ViewModels
{
    public class FeedViewModel
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public int ItemCount { get; set; }

        public string UnreadClass { get; set; }
    }
}