using System.Collections.Generic;
using Lucene.Net.Store;

namespace Rss.Indexer
{
    public interface ISearchConfig
    {
        Directory Directory { get; }

        int SearchResultLimit { get; }
    }
}