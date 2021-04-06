using Microsoft.EntityFrameworkCore;

namespace Rss.Api.Data
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options)
            : base(options) { }

        public DbSet<Folder> Folders { get; set; }
        public DbSet<Feed> Feeds { get; set; }
        public DbSet<Item> Items { get; set; }
    }
}
