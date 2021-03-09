using System;
using System.Threading.Tasks;
using Rss.Server.Models;
using System.Threading.Tasks;

namespace Rss.Server.Services
{
    public interface IFeedService
    {
        Feed Get(Guid id, ReadOptions readOptions);

        void Unsubscribe(Guid id);

        void Rename(Guid id, string name);

        void Mark(Guid id, MarkOptions markOptions);

        Task<bool> Refresh(Guid id, bool force = false);

        Guid Add(Uri feedUrl, Guid? folderId);
    }
}