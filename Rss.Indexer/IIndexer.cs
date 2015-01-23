using System.Collections.Generic;
using Lucene.Net.Analysis;
using Lucene.Net.Store;

namespace Rss.Indexer
{
    public interface IIndexer<T>
    {
        int IndexBatchSize { get; }

        Directory Directory { get; }

        Analyzer Analyzer { get; }

        IEnumerable<T> GetDocumentsToIndex();

        void SetDocumentsIndexed(IEnumerable<T> documents);
    }
}