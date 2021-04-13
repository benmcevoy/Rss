using System;
using System.ComponentModel.DataAnnotations;

namespace Rss.Api.Data
{
    public class Item
    {
        [Key]
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Raw { get; set; }
        public string Content { get; set; }
        public string Snippet { get; set; }
        public DateTime? ReadDateTime { get; set; }
        public DateTime PublishedDateTime { get; set; }
        public string LinkUrl { get; set; }
        public Guid FeedId { get; set; }
        public Feed Feed { get; set; }
    }
}