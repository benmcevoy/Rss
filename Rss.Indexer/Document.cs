using System;

namespace Rss.Indexer
{
    // TODO: this class should be in the consumer project
    [LuceneDocument("Id")]
    public class Document
    {
        public string[] Tags { get; set; }

        [LuceneField("Title")]
        public string Title { get; set; }

        [LuceneField("Content")]
        public string Content { get; set; }

        //[LuceneField("Id")]
        public Guid Id { get; set; }
    }
}
