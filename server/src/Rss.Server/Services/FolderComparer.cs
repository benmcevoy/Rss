using System.Collections.Generic;
using Rss.Server.Models;

namespace Rss.Server.Services
{
    public class FolderComparer : IEqualityComparer<Folder>
    {
        public bool Equals(Folder x, Folder y)
        {
            return x.Id == y.Id;
        }

        public int GetHashCode(Folder obj)
        {
            return obj.Id.GetHashCode();
        }
    }
}
