using Lucene.Net.Index;
using System;

namespace Rss.Indexer
{
    [AttributeUsage(AttributeTargets.Class)]
    public class LuceneDocumentAttribute : Attribute
    {
        public LuceneDocumentAttribute(string documentIdFieldName)
        {
            DocumentIdFieldName = documentIdFieldName;
        }

        public string DocumentIdFieldName { get; private set; }

        public Term IdTerm { get { return new Term(DocumentIdFieldName); } }
    }
}
