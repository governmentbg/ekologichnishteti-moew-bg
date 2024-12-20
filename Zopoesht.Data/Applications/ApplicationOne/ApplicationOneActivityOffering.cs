using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Zopoesht.Data.Common.Interfaces;
using Zopoesht.Data.Nomenclatures.Operators;

namespace Zopoesht.Data.Applications.ApplicationOne
{
    public class ApplicationOneActivityOffering : IEntity
    {
        public int Id { get; set; }

        public int ApplicationOneBasicId { get; set; }
        public ApplicationOneBasic ApplicationOneBasic { get; set; }

        public int ActivityOfferingId { get; set; }
        public ActivityOffering ActivityOffering { get; set; }
    }

    public class ApplicationOneAcivityOfferingConfiguration : IEntityTypeConfiguration<ApplicationOneActivityOffering>
    {
        public void Configure(EntityTypeBuilder<ApplicationOneActivityOffering> builder)
        {
            builder.HasOne(e => e.ApplicationOneBasic)
                   .WithMany(e => e.ActivityOfferings)
                   .HasForeignKey(e => e.Id);
        }
    }
}
