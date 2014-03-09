using System;
using Lucene.Net.Documents;

namespace Rss.Indexer
{
    [AttributeUsage(AttributeTargets.Property)]
    public class LuceneFieldAttribute : Attribute
    {
        public LuceneFieldAttribute(string name, Field.Store store = Field.Store.YES,
            Field.Index index = Field.Index.ANALYZED, Field.TermVector termVector = Field.TermVector.NO)
        {
            Store = store;
            Index = index;
            TermVector = termVector;
            Name = name;
        }

        public string Name { get; private set; }        
        
        public Field.Store Store { get; private set; }
        
        public Field.Index Index { get; private set; }
        
        public Field.TermVector TermVector { get; private set; }
    }
}
