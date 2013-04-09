using System;
using System.Collections.Generic;
using Rss.Server.Models;

namespace Rss.Server.Services
{
    public interface IItemService
    {
        Item Get(Guid id);

        IEnumerable<Item> Get();

        void Read(Guid id);
    }
}