using System;

namespace Rss.Indexer
{
    public class Document
    {
        public string[] Tags { get; set; }

        [LuceneField("Title")]
        public string Title { get; set; }

        [LuceneField("Content")]
        public string Content { get; set; }

        [LuceneField("Id")]
        public Guid Id { get; set; }
    }
}
