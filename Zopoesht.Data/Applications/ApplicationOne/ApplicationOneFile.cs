using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Zopoesht.Data.Attributes;
using Zopoesht.Data.Common.Interfaces;
using Zopoesht.Data.Common.Models;

namespace Zopoesht.Data.Applications.ApplicationOne
{
    public class ApplicationOneFile : IEntity
    {
        public int Id { get; set; }
        public string Description { get; set; }

        public int? ApplicationOneId { get; set; }
        public ApplicationOne ApplicationOne { get; set; }

        public int ZopoeshtAttachedFileId { get; set; }
        public ZopoeshtAttachedFile ZopoeshtAttachedFile { get; set; }

    }
    public class ApplicationOneFileConfiguration : IEntityTypeConfiguration<ApplicationOneFile>
    {
        public void Configure(EntityTypeBuilder<ApplicationOneFile> builder)
        {
            builder.HasOne(e => e.ApplicationOne)
                   .WithMany(e => e.ApplicationOneFiles)
                   .HasForeignKey(e => e.Id);
        }
    }
}
