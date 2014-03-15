using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Lucene.Net.Index;
using Lucene.Net.Search;

namespace Rss.Indexer
{
    public class Searcher<T> where T : new()
    {
        private readonly ISearchConfig _searchConfig;

        public Searcher(ISearchConfig searchConfig)
        {
            _searchConfig = searchConfig;
        }

        public virtual IEnumerable<T> Search(string query)
        {
            query = ToSafeQuery(query);

            var searcher = new IndexSearcher(_searchConfig.Directory);
            var topDocs = searcher
                .Search(ToWildCardQuery(query, new T().GetLuceneFieldInfos()), _searchConfig.SearchResultLimit);

            return topDocs.ScoreDocs
                .Select(scoreDoc => 
                    searcher.Doc(scoreDoc.Doc).ToResult<T>());
        }

        protected Query ToWildCardQuery(string query, IEnumerable<LuceneFieldInfo> fields)
        {
            var terms = query.ToLowerInvariant().Split(new[] { " ,." }, StringSplitOptions.RemoveEmptyEntries);
            var wildCardQuery = new BooleanQuery();

            terms.ForEach(term =>
            {
                var booleanQuery = new BooleanQuery();
                
                fields.ForEach(field =>
                {
                    var subQuery = new FuzzyQuery(
                        new Term(field.LuceneFieldAttribute.Name, term),
                        field.LuceneFieldAttribute.Fuzziness)
                    {
                        Boost = field.LuceneFieldAttribute.Boost
                    };

                    booleanQuery.Add(subQuery, Occur.SHOULD);
                });

                wildCardQuery.Add(booleanQuery, Occur.MUST);
            });

            return wildCardQuery;
        }

        protected static string ToSafeQuery(string query)
        {
            // http://lucene.apache.org/core/2_9_4/queryparsersyntax.html#Escaping%20Special%20Characters
            // remove any special lucene characters
            // + - && || ! ( ) { } [ ] ^ " ~ * ? : \
            return Regex.Replace(query, @"[\?\*\|\+\-!\(\)\{\}\[\]&:\#\\""~\^]", "");
        }
    }
}
