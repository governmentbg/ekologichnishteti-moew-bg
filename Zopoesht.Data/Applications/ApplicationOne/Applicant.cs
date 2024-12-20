using Zopoesht.Data.Applications.ApplicationOne.Enums;
using Zopoesht.Data.Common.Interfaces;
using Zopoesht.Data.Nomenclatures;
using Zopoesht.Data.Nomenclatures.Operators;

namespace Zopoesht.Data.Applications.ApplicationOne
{
    public class Applicant : IEntity
    {
        public int Id { get; set; }

        public ApplicantType ApplicantType { get; set; }

        public int? AuthorityId { get; set; }
        public Authority Authority { get; set; }

        public int? OperatorId { get; set; }
        public Operator Operator { get; set; }

        public int? IndividualId { get; set; }
        public Individual Individual { get; set; }

        public int? LegalEntityId { get; set; }
        public LegalEntity LegalEntity { get; set; }

    }
}
