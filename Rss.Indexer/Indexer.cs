using System.Diagnostics;
using System.Linq;
using Lucene.Net.Index;

namespace Rss.Indexer
{
    public class Indexer<T>
    {
        private readonly IIndexer<T> _indexConfig;

        public Indexer(IIndexer<T> indexConfig)
        {
            _indexConfig = indexConfig;
        }

        public virtual void Index()
        {
            var results = _indexConfig.GetDocumentsToIndex().ToList();
            var count = results.Count;

            while (results.Any())
            {
                Debug.WriteLine("Indexer processing - {0} documents found", count);
                // commit in batches
                // TODO: read the docs on indexwriter
                using (
                    var indexWriter = new IndexWriter(_indexConfig.Directory, _indexConfig.Analyzer,
                        IndexWriter.MaxFieldLength.LIMITED))
                {
                    results.ForEach(doc => Index(indexWriter, doc));

                    _indexConfig.SetDocumentsIndexed(results);

                    indexWriter.Commit();
                }

                results = _indexConfig.GetDocumentsToIndex().ToList();

                count += results.Count;
            }
        }

        protected virtual void Index(IndexWriter indexWriter, T document)
        {
            indexWriter.UpdateDocument(document.GetLuceneDocumentIdTerm(), document.ToLuceneDocument());
        }
    }
}
