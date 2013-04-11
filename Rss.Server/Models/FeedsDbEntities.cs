using System.Data.Entity;
using Rss.Server.Models.Mappings;

namespace Rss.Server.Models
{
    public class FeedsDbEntities : DbContext
    {
        public FeedsDbEntities()
            : base("name=FeedsDBEntities")
        {
            Configuration.LazyLoadingEnabled = true;
            Configuration.ProxyCreationEnabled = true;
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new FolderMap());
            modelBuilder.Configurations.Add(new FeedMap()); 
            modelBuilder.Configurations.Add(new ItemMap());
        }

        public DbSet<Feed> Feeds { get; set; }

        public DbSet<Folder> Folders { get; set; }

        public virtual DbSet<Item> Items { get; set; }
    }
}
