using System.Data.Entity.ModelConfiguration;

namespace Rss.Server.Models.Mappings
{
    public class FolderMapping : EntityTypeConfiguration<Folder>
    {
        public FolderMapping()
        {
            HasKey(x => x.Id);

            HasMany(x => x.Feeds).WithOptional(x => x.Folder).WillCascadeOnDelete(true);

            ToTable("Folder");
        }
    }
}