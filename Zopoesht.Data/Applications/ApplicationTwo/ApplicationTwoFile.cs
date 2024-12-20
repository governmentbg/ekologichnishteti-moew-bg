using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Zopoesht.Data.Attributes;
using Zopoesht.Data.Common.Interfaces;
using Zopoesht.Data.Common.Models;

namespace Zopoesht.Data.Applications.ApplicationTwo
{
    public class ApplicationTwoFile : IEntity
    {
        public int Id { get; set; }
        public string Description { get; set; }

        public int? ApplicationTwoId { get; set; }
        public ApplicationTwo ApplicationTwo { get; set; }

        public int ZopoeshtAttachedFileId { get; set; }
        public ZopoeshtAttachedFile ZopoeshtAttachedFile { get; set; }

    }

    public class ApplicationTwoFileConfiguration : IEntityTypeConfiguration<ApplicationTwoFile>
    {
        public void Configure(EntityTypeBuilder<ApplicationTwoFile> builder)
        {
            builder.HasOne(e => e.ApplicationTwo)
                   .WithMany(e => e.ApplicationTwoFiles)
                   .HasForeignKey(e => e.Id);
        }
    }

}
