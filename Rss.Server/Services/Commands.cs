using System;
using Rss.Server.Models;

namespace Rss.Server.Services
{
    public static class Commands
    {
        public static void MarkFeedAsReadCommand(this FeedsDbEntities context, Guid feedId, MarkOptions markOptions)
        {
            context.Database.ExecuteSqlCommand("UPDATE [Item] SET [ReadDateTime] = {0} WHERE [FeedId] = {1} AND [ReadDateTime] IS NULL", 
                DateTime.Now, feedId);
        }
    }
}