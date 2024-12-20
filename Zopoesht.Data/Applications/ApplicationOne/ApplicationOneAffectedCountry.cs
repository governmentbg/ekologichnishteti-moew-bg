using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Zopoesht.Data.Common.Interfaces;
using Zopoesht.Data.Nomenclatures;

namespace Zopoesht.Data.Applications.ApplicationOne
{
    public class ApplicationOneAffectedCountry : IEntity
    {
        public int Id { get; set; }

        public int ApplicationOneBasicId { get; set; }
        public ApplicationOneBasic ApplicationOneBasic { get; set; }

        public int CountryId { get; set; }
        public Country Country { get; set; }

        public class AApplicationOneAffectedCountryConfiguration : IEntityTypeConfiguration<ApplicationOneAffectedCountry>
        {
            public void Configure(EntityTypeBuilder<ApplicationOneAffectedCountry> builder)
            {
              
            }
        }
    }
}
