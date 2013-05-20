using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Rss.Server.PostModel
{
    public class MoveFeedToFolderDto
    {
        public Guid FeedId { get; set; }

        public Guid FolderId { get; set; }
    }
}