//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Rss.Server.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Item
    {
        public System.Guid Id { get; set; }
        public string Raw { get; set; }
        public string Name { get; set; }
        public string Content { get; set; }
        public Nullable<System.DateTime> ReadDateTime { get; set; }
        public System.Guid FeedId { get; set; }
        public string Snippet { get; set; }
        public Nullable<System.DateTime> PublishedDateTime { get; set; }
        public string LinkId { get; set; }
    }
}
