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

            while (results.Any())
            {
                results.ForEach(Index);

                _indexWriter.Commit();
                _indexConfig.SetDocumentsIndexed(results);
                
                results = _indexConfig.GetDocumentsToIndex().ToList();
            }
        }

        private void Index(T document)
        {
            _indexWriter.AddDocument(document.ToLuceneDocument());
        }
    }
}
