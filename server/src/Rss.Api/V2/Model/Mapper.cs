using System.Linq;

namespace Rss.Api.V2.Model
{
    public class Mapper
    {
        public static Folder Map(Data.Folder source)
        {
            return new Folder
            {
                Id = source.Id,
                Name = source.Name,
                Feeds = source.Feeds.Select(Map).ToList(),
            };
        }

        public static Feed Map(Data.Feed source)
        {
            return new Feed
            {
                Id = source.Id,
                Name = source.Name,
                LastUpdatedDateTime = source.LastUpdateDateTime,
                WebsiteUrl = source.HtmlUrl,
                Count = source.Items.Count,
                FavIcon = source.FavIcon
            };
        }

        public static RssItem Map(Data.Item source)
        {
            return new RssItem
            {
                Id = source.Id,
                Name = source.Name,
                Content = source.Content,
                FeedId = source.FeedId,
                PublishedDateTime = source.PublishedDateTime,
                Snippet = source.Snippet,
                Tags = [source.Feed.Folder?.Name ?? ""],
                FolderId = source.Feed.FolderId,
                Url = source.LinkUrl
            };
        }

        public static Article MapArticle(Data.Item source)
        {
            return new Article
            {
                Id = source.Id,
                Name = source.Name,
                Content = source.Content,
                FeedId = source.FeedId,
                PublishedDateTime = source.PublishedDateTime,
                Snippet = source.Snippet,
                Tags = [source.Feed.Folder?.Name ?? ""],
                FolderId = source.Feed.FolderId,
                Url = source.LinkUrl,
                FeedFavIcon = source.Feed.FavIcon,
                FeedName = source.Feed.Name,
                FeedWebsiteUrl = source.Feed.HtmlUrl
            };
        }
    }
}
