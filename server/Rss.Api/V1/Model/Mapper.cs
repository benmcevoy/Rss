using System.Linq;

namespace Rss.Api.V1.Model
{
    public class Mapper
    {
        public static Folder Map(Data.Folder source)
        {
            return new Folder
            {
                Id = source.Id,
                Name = source.Name,
                Feeds = source.Feeds.Select(Map).ToList()
            };
        }

        public static Feed Map(Data.Feed source)
        {
            return new Feed
            {
                Id = source.Id,
                Name = source.Name,
                LastUpdatedDateTime = source.LastUpdateDateTime,
                WebsiteUrl = source.HtmlUrl
            };
        }

        public static RssItem Map(Data.Item source)
        {
            return new RssItem
            {
                Id = source.Id,
                Name = source.Name,
                FeedName = source.Feed.Name,
                Content = source.Content,
                FeedId = source.FeedId,
                FeedLastUpdatedDateTime = source.Feed.LastUpdateDateTime,
                FeedWebsiteUrl = source.Feed.HtmlUrl,
                PublishedDateTime = source.PublishedDateTime,
                Snippet = source.Snippet,
                Tags = new [] { source.Feed.Folder?.Name ?? "" }
            };
        }
    }
}
