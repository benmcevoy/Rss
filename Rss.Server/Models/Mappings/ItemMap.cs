using System.Data.Entity.ModelConfiguration;

namespace Rss.Server.Models.Mappings
{
    public class ItemMap : EntityTypeConfiguration<Item>
    {
        public ItemMap()
        {
            HasKey(x => x.Id);

            Property(x => x.Id).IsRequired();
            Property(x => x.Name).HasMaxLength(2000).IsRequired();
            Property(x => x.Content);
            Property(x => x.Raw);
            Property(x => x.ReadDateTime);
            Property(x => x.FeedId).IsRequired();
            Property(x => x.Snippet);
            Property(x => x.PublishedDateTime);
            Property(x => x.LinkUrl).HasColumnName("LinkId");
            Property(x => x.Name);

            ToTable("Item");
        }
    }
}