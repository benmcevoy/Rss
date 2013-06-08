using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

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