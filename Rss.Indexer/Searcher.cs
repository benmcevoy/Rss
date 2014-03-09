using System.Collections.Generic;
using System.Linq;
using Lucene.Net.Index;
using Lucene.Net.Search;

namespace Rss.Indexer
{
    public class Searcher<T> where T: new()
    {
        private readonly ISearchConfig _searchConfig;

        public Searcher(ISearchConfig searchConfig)
        {
            _searchConfig = searchConfig;
        }

        public virtual IEnumerable<T> Search(string query)
        {
            var searcher = new IndexSearcher(_searchConfig.Directory);
            var topDocs = searcher
                .Search(ToWildCardQuery(query, _searchConfig.Fields), _searchConfig.SearchResultLimit);

            return topDocs.ScoreDocs
                .Select(scoreDoc => searcher.Doc(scoreDoc.Doc).ToResult<T>());
        }

        protected Query ToWildCardQuery(string query, string[] fields)
        {
            var terms = query.ToLowerInvariant().Split(' ');
            var result = new BooleanQuery();

            // terms may be in any field but must appear
            // (name:evening* OR description:evening*) AND (name:design* OR description:design*)
            foreach (var term in terms)
            {
                var termQuery = new BooleanQuery();

                foreach (var field in fields)
                {
                    termQuery.Add(new WildcardQuery(
                        new Term(field, term.Trim() + "*")),
                        Occur.SHOULD);
                }

                result.Add(termQuery, Occur.MUST);
            }

            return result;
        }
    }
}
