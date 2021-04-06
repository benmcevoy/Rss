using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Rss.Api.Data
{
    public class Folder
    {
        [Key]
        public Guid Id { get; set; }
        public string Name { get; set; }
        public DateTime LastUpdateDateTime { get; set; }
        public List<Feed> Feeds { get; set; } = new List<Feed>();
    }
}