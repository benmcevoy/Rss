using System;
using System.Collections.Generic;

namespace Rss.Manager
{
    public interface IRssManager
    {
        IEnumerable<Feed> Feeds { get; }

        void Subscribe(Feed feed);

        void Subscribe(Uri feedUri, string title, Uri htmlUri);

        void Unsubscribe(Feed feed);
    }
}