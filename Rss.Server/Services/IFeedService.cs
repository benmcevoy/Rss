﻿using System;
using Rss.Server.Models;
<<<<<<< HEAD
=======
using System.Threading.Tasks;
>>>>>>> 391d87e017ca9adeb120e9805ce0db87310cca88

namespace Rss.Server.Services
{
    public interface IFeedService
    {
        Feed Get(Guid id, ReadOptions readOptions);

        void Unsubscribe(Guid id);

        void Rename(Guid id, string name);

        void Mark(Guid id, MarkOptions markOptions);

        void Refresh(Guid id, bool force = false);

        Guid Add(Uri feedUrl, Guid? folderId);
    }
}