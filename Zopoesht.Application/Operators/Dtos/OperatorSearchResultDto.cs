using Zopoesht.Data.Nomenclatures;
using Zopoesht.Data.Nomenclatures.Operators.Enums;
using Zopoesht.Data.Nomenclatures.Settlements;

namespace Zopoesht.Application.Operators.Dtos
{
    public class OperatorSearchResultDto
    {
        public int Id { get; set; }
        public string Fullname { get; set; }
        public OperatorType? Type { get; set; }
        public string LegalEntityUic { get; set; }
        public int? SettlementId { get; set; }
        public Settlement Settlement { get; set; }
        public int? AuthorityRiosvId { get; set; }
        public Authority AuthoriyRiosv { get; set; }
        public int? AuthorityBasinId { get; set; }
        public Authority AuthorityBasin { get; set; }
    }
}
