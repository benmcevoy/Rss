using System;
using System.Collections.Generic;
using Rss.Server.Models;

namespace Rss.Server.Services
{
    public interface IFolderService
    {
        Folder Get(Guid id);

        Folder Get(string folderName);

        RootFolder GetRoot();

        IEnumerable<Item> GetItems(Guid id, ReadOptions readOptions);

        void AddFeed(Guid id, Guid feedId);

        void RemoveFeed(Guid id, Guid feedId);

        void Delete(Guid id);

        void Rename(Guid id, string name);

        Folder Create(string name);

        void Refresh(Guid id);

        void Mark(Guid id, MarkOptions markOptions);
    }
}