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
    
    public partial class Folder
    {
        public Folder()
        {
            this.Feeds = new HashSet<Feed>();
        }
    
        public System.Guid Id { get; set; }
        public string Name { get; set; }
        public Nullable<System.DateTime> LastUpdateDateTime { get; set; }
    
        public virtual ICollection<Feed> Feeds { get; set; }
    }
}
