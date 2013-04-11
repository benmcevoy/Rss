namespace Rss.Manager
{
    public class Item
    {
        public Item(string id, string raw, string title, string publishedDateTime)
        {
            Id = id;
            Raw = raw;
            Content = HtmlCleaner.Clean(raw);
            Snippet = HtmlCleaner.GetSnippet(raw, 200);
            Title = title;
            PublishedDateTime = publishedDateTime;
        }

        public string Id { get; set; }

        public string Raw { get; set; }

        public string Content { get; private set; }

        public string Snippet { get; private set; }

        public string Title { get; set; }

        public string PublishedDateTime { get; set; }
    }
}