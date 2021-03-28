using System.Collections.Generic;

namespace Radio7.Rss
{
    public interface IRssManager
    {
        IEnumerable<Feed> Feeds { get; }

        void Subscribe(Feed feed);

        void Unsubscribe(Feed feed);
    }
}