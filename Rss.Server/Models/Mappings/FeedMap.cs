using System.Data.Entity.ModelConfiguration;

namespace Rss.Server.Models.Mappings
{
    public class FeedMap: EntityTypeConfiguration<Feed>
    {
        public FeedMap()
        {
            HasKey(x => x.Id);

            Property(x => x.Id).IsRequired();
            Property(x => x.Name).HasMaxLength(2000).IsRequired();
            Property(x => x.LastUpdateDateTime);
            Property(x => x.FolderId);
            Property(x => x.FavIcon);
            Property(x => x.FeedUrl);
            Property(x => x.HtmlUrl);
            Property(x => x.LastBuildDate);
            Property(x => x.LastUpdateDateTime);
            Property(x => x.Name);
            Property(x => x.UpdateFrequency);
            Property(x => x.UpdatePeriod);

            HasMany(x => x.Items).WithOptional();

            ToTable("Feed");
        }
    }
}