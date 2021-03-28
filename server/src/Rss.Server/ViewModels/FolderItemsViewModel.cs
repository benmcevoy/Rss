using Rss.Server.Models;
using System;
using System.Collections.Generic;

namespace Rss.Server.ViewModels
{
    public class FolderItemsViewModel
    {
        public string FolderName { get; set; }

        public Guid FolderId { get; set; }
        
        public IEnumerable<Item> Items { get; set; }     
    }
}