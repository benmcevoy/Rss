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

        public static void MarkFolderAsReadCommand(this FeedsDbEntities context, Guid folderId, MarkOptions markOptions)
        {
            context.Database.ExecuteSqlCommand(@"
UPDATE       Item
SET          ReadDateTime = {0}
WHERE        (Id IN
             (SELECT        Item_1.Id
             FROM            Item AS Item_1 
                INNER JOIN Feed ON Feed.Id = Item_1.FeedId 
                INNER JOIN Folder ON Folder.Id = Feed.FolderId
            WHERE        (Item_1.ReadDateTime IS NULL) AND (Folder.Id = {1})))",
                DateTime.Now, folderId);
        }
    }
}