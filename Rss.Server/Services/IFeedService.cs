using System;
using Rss.Server.Models;

namespace Rss.Server.Services
{
    public interface IFeedService
    {
        Feed Get(Guid id, ReadOptions readOptions);

        void Unsubscribe(Guid id);

        void Rename(Guid id, string name);

        void Mark(Guid id, MarkOptions markOptions);

        void Refresh(Guid id);
    }
}