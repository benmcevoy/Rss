using System;
using Rss.Server.Models;

namespace Rss.Server.Services
{
    public interface IFolderService
    {
        Folder Get(Guid id);

        RootFolder GetRoot();

        void AddFeed(Guid id, Guid feedId);

        void RemoveFeed(Guid id, Guid feedId);

        void Delete(Guid id);

        void Rename(Guid id, string name);

        void Create(string name);
    }
}