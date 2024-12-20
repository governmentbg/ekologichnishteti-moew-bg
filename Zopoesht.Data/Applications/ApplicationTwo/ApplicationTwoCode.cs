using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Zopoesht.Data.Common.Interfaces;
using Zopoesht.Data.Nomenclatures;

namespace Zopoesht.Data.Applications.ApplicationTwo
{
    public class ApplicationTwoCode : IEntity
    {
        public int Id { get; set; }

        public int ApplicationTwoId { get; set; }
        public ApplicationTwo ApplicationTwo { get; set; }

        public int CodeId { get; set; }
        public Code Code { get; set; }
    }

    public class ApplicationTwoCodeConfiguration : IEntityTypeConfiguration<ApplicationTwoCode>
    {
        public void Configure(EntityTypeBuilder<ApplicationTwoCode> builder)
        {
            builder.HasOne(e => e.ApplicationTwo)
                   .WithMany(e => e.Codes)
                   .HasForeignKey(e => e.Id);
        }
    }
}
