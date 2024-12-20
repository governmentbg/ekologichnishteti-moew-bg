using System.Linq.Expressions;
using Zopoesht.Application.Common.Interfaces;
using Zopoesht.Application.Nomenclatures.Dtos;
using Zopoesht.Data.Nomenclatures;
using Zopoesht.Data.Nomenclatures.Operators;
using Zopoesht.Data.Nomenclatures.Operators.Enums;
using Zopoesht.Data.Nomenclatures.Settlements;

namespace Zopoesht.Application.Operators.Dtos
{
    public class OperatorNomenclatureMapperDto : NomenclatureMapperDto<Operator>, IMapping<Operator, OperatorNomenclatureMapperDto>
    {
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string LegalEntityName { get; set; }
        public string LegalEntityUic { get; set; }
        public OperatorType Type { get; set; }
        public int? SettlementId { get; set; }
        public Settlement Settlement { get; set; }
        public string ManagementAddress { get; set; }
        public int OperatorCorrespondenceId { get; set; }
        public OperatorCorrespondence OperatorCorrespondence { get; set; }

        public ICollection<ActivityOffering> ActivityOfferings { get; set; } = new List<ActivityOffering>();

        Expression<Func<Operator, OperatorNomenclatureMapperDto>> IMapping<Operator, OperatorNomenclatureMapperDto>.Map()
        {
            return e => new OperatorNomenclatureMapperDto
            {
                Id = e.Id,
                Name = e.Name,
                Alias = e.Alias,
                IsActive = e.IsActive,
                FirstName = e.FirstName,
                MiddleName = e.MiddleName,
                LastName = e.LastName,
                ManagementAddress = e.ManagementAddress,
                LegalEntityName = e.LegalEntityName,
                LegalEntityUic = e.LegalEntityUic,
                Type = e.Type,
                SettlementId = e.SettlementId,
                Settlement = e.Settlement,
                OperatorCorrespondenceId = e.OperatorCorrespondenceId,
                OperatorCorrespondence = e.OperatorCorrespondence,
                ActivityOfferings = e.ActivityOfferings
            };
        }
    }
}
