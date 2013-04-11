using System;
using System.Collections.Generic;
using Rss.Server.Models;

namespace Rss.Server.Services
{
    public interface IItemService
    {
        Item Get(Guid id);

        IEnumerable<Item> Get(int count);

        void Read(Guid id);
    }
}