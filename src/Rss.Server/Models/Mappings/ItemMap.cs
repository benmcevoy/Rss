using System.Data.Entity.ModelConfiguration;

namespace Rss.Server.Models.Mappings
{
    public class ItemMap : EntityTypeConfiguration<Item>
    {
        public ItemMap()
        {
            HasKey(x => x.Id);
            
            Property(x => x.Content).HasColumnType("ntext").IsMaxLength();
            Property(x => x.Raw).HasColumnType("ntext").IsMaxLength();
            Property(x => x.LinkUrl).HasColumnName("LinkId");

            ToTable("Item");
        }
    }
}