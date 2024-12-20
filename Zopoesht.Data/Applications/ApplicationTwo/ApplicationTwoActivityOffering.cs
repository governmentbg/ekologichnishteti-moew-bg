using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Zopoesht.Data.Common.Interfaces;
using Zopoesht.Data.Nomenclatures.Operators;

namespace Zopoesht.Data.Applications.ApplicationTwo
{
    public class ApplicationTwoActivityOffering : IEntity
    {
        public int Id { get; set; }

        public int ApplicationTwoId { get; set; }
        public ApplicationTwo ApplicationTwo { get; set; }

        public int ActivityOfferingId { get; set; }
        public ActivityOffering ActivityOffering { get; set; }
    }

    public class ApplicationOTwAcivityOfferingConfiguration : IEntityTypeConfiguration<ApplicationTwoActivityOffering>
    {
        public void Configure(EntityTypeBuilder<ApplicationTwoActivityOffering> builder)
        {
            builder.HasOne(e => e.ApplicationTwo)
                   .WithMany(e => e.ApplicationTwoActivityOfferings)
                   .HasForeignKey(e => e.Id);
        }
    }
}
