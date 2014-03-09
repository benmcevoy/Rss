using System.Reflection;

namespace Rss.Indexer
{
    public class LuceneFieldInfo
    {
        public PropertyInfo PropertyInfo { get; set; }

        public LuceneFieldAttribute LuceneFieldAttribute { get; set; }
    }
}
