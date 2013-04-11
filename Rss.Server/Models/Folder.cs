using System;
using System.Collections.Generic;

namespace Rss.Server.Models
{
    public class Folder
    {
        public Folder()
        {
            Feeds = new HashSet<Feed>();
        }
    
        public Guid Id { get; set; }

        public string Name { get; set; }

        public DateTime? LastUpdateDateTime { get; set; }
    
        public virtual ICollection<Feed> Feeds { get; set; }
    }
}
