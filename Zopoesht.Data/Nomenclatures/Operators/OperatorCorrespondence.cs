using Zopoesht.Data.Attributes;
using Zopoesht.Data.Common.Interfaces;
using Zopoesht.Data.Nomenclatures.Settlements;

namespace Zopoesht.Data.Nomenclatures.Operators
{
    public class OperatorCorrespondence : IEntity
    {
        public int Id { get; set; }
        public int? SettlementId { get; set; }
        [Skip]
        public Settlement Settlement { get; set; }
        public string CorrespondenceAddress { get; set; }
        public string Phone { get; set; }
        public string Fax { get; set; }
        public string Email { get; set; }
        public string ContactPerson { get; set; }
        public string PostalCode { get; set; }
    }
}
