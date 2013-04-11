using System.Data.Entity.ModelConfiguration;

namespace Rss.Server.Models.Mappings
{
    public class FolderMap : EntityTypeConfiguration<Folder>
    {
        public FolderMap()
        {
            HasKey(x => x.Id);

            Property(x => x.Id).IsRequired();
            Property(x => x.Name).HasMaxLength(2000).IsRequired();
            Property(x => x.LastUpdateDateTime);

            HasMany(x => x.Feeds).WithOptional();

            ToTable("Folder");
        }
    }
}