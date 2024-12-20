using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Zopoesht.Data.Common.Interfaces;
using Zopoesht.Data.Nomenclatures;

namespace Zopoesht.Data.Applications.ApplicationOne
{
    public class ApplicationOneTerritorialRange : IEntity
    {
        public int Id { get; set; }

        public int ApplicationOneBasicId { get; set; }
        public ApplicationOneBasic ApplicationOneBasic { get; set; }

        public int AuthorityId { get; set; }
        public Authority Authority { get; set; }
    }

    public class ApplicationOneTerritorialRangeConfiguration : IEntityTypeConfiguration<ApplicationOneTerritorialRange>
    {
        public void Configure(EntityTypeBuilder<ApplicationOneTerritorialRange> builder)
        {
            builder.HasOne(e => e.ApplicationOneBasic)
                   .WithMany(e => e.TerritorialRanges)
                   .HasForeignKey(e => e.Id);
        }
    }
}
