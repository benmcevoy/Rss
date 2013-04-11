using System.Data.Entity;
using Rss.Server.Models.Mappings;

namespace Rss.Server.Models
{
    public class FeedsDbEntities : DbContext
    {
        public FeedsDbEntities()
            : base("name=FeedsDbEntities")
        {
            Configuration.LazyLoadingEnabled = false;
            Configuration.ProxyCreationEnabled = true;
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            // defaults
            modelBuilder.Entity<Folder>().ToTable("Folder");
            modelBuilder.Entity<Feed>().ToTable("Feed");
            // explicit
            modelBuilder.Configurations.Add(new ItemMap());
        }

        public DbSet<Feed> Feeds { get; set; }

        public DbSet<Folder> Folders { get; set; }

        public virtual DbSet<Item> Items { get; set; }
    }
}
