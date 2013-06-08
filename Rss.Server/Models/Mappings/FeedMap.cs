using System.Data.Entity.ModelConfiguration;

namespace Rss.Server.Models.Mappings
{
    public class FeedMap : EntityTypeConfiguration<Feed>
    {
        public FeedMap()
        {
            HasKey(x => x.Id);

            Ignore(x => x.ItemCount);

            HasMany(x => x.Items).WithRequired(x => x.Feed).WillCascadeOnDelete(true);

            ToTable("Feed");
        }
    }
}