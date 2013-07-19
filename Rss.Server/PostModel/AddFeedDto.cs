using System;

namespace Rss.Server.PostModel
{
    public class AddFeedDto
    {
        public string Folder { get; set; }

        public Uri Url { get; set; }
    }
}