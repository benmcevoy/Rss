using System.Diagnostics;
using System.Linq;
using Lucene.Net.Index;

namespace Rss.Indexer
{
    public class Indexer<T>
    {
        private readonly IIndexer<T> _indexConfig;
        private readonly IndexWriter _indexWriter;

        public Indexer(IIndexer<T> indexConfig)
        {
            _indexConfig = indexConfig;
            _indexWriter = new IndexWriter(_indexConfig.Directory, _indexConfig.Analyzer, IndexWriter.MaxFieldLength.LIMITED);
        }

        public void Index()
        {
            var results = _indexConfig.GetDocumentsToIndex().ToList();
            var count = results.Count;

            while (results.Any())
            {
                Debug.WriteLine("Indexer processing - {0} documents found", count);

                results.ForEach(Index);

                _indexWriter.Commit();
                _indexConfig.SetDocumentsIndexed(results);
                
                results = _indexConfig.GetDocumentsToIndex().ToList();

                count += results.Count;
            }
        }

        private void Index(T document)
        {
            _indexWriter.AddDocument(document.ToLuceneDocument());
        }
    }
}
