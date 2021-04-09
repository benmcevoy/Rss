using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Rss.Api.Data.Services
{
    public class MarkAsReadDataService
    {
        private readonly DatabaseContext _databaseContext;

        public MarkAsReadDataService(DatabaseContext databaseContext) => _databaseContext = databaseContext;

        public async Task MarkAsReadAll()
        {
            await _databaseContext.Items
                .Where(item => item.ReadDateTime == null)
                .ForEachAsync(item => item.ReadDateTime = DateTime.UtcNow);

            await _databaseContext.SaveChangesAsync();
        }

        public async Task MarkAsReadFolder(Guid id)
        {
            var items = _databaseContext.Folders
                .Include(x => x.Feeds)
                .ThenInclude(x => x.Items)
                .Single(folder => folder.Id == id)
                .Feeds
                .SelectMany((feed, i) => feed.Items.Where(i => i.ReadDateTime == null));

            foreach (var item in items)
            {
                item.ReadDateTime = DateTime.UtcNow;
            }

            await _databaseContext.SaveChangesAsync();
        }
        
        public async Task MarkAsReadFeed(Guid id)
        {
            var items = _databaseContext.Feeds
                .Include(x => x.Items)
                .Single(f=> f.Id == id)
                .Items.Where(i => i.ReadDateTime == null);

            foreach (var item in items)
            {
                item.ReadDateTime = DateTime.UtcNow;
            }

            await _databaseContext.SaveChangesAsync();
        }
    }
}
