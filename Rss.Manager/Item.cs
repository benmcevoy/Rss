namespace Rss.Manager
{
    public class Item
    {
        public Item(string id, string description, string title, string publishedDateTime)
        {
            Id = id;
            Description = description;
            Title = title;
            PublishedDateTime = publishedDateTime;
        }

        public string Id { get; set; }

        public string Description { get; set; }

        public string Title { get; set; }

        public string PublishedDateTime { get; set; }
    }
}