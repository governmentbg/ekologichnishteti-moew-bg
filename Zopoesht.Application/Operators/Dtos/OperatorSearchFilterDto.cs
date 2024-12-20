using Microsoft.EntityFrameworkCore;
using Zopoesht.Application.Common.Dtos;
using Zopoesht.Data.Nomenclatures.Operators;
using Zopoesht.Data.Nomenclatures.Operators.Enums;

namespace Zopoesht.Application.Operators.Dtos
{
    public class OperatorSearchFilterDto: FilterDto<Operator>
    {
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public OperatorType? Type { get; set; }
        public string LegalEntityName { get; set; }
        public string LegalEntityUic { get; set; }
        public int? SettlementId { get; set; }
        public int? AuthorityRiosvId { get; set; }
        public int? AuthorityBasinId { get; set; }

        public override IQueryable<Operator> WhereBuilder(IQueryable<Operator> query)
        {
            if (Type.HasValue)
            {
                if (Type.Value == OperatorType.Person)
                {
                    query = query.Where(e => e.Type == OperatorType.Person);
                }

                if (Type.Value == OperatorType.LegalEntity)
                {
                    query = query.Where(e => e.Type == OperatorType.LegalEntity);
                }
            }

            if (!string.IsNullOrWhiteSpace(FirstName))
            {
                var textFilter = $"%{FirstName.Trim().ToLower()}%";
                query = query.Where(e => EF.Functions.ILike(e.FirstName, textFilter));
            }

            if (!string.IsNullOrWhiteSpace(MiddleName))
            {
                var textFilter = $"%{MiddleName.Trim().ToLower()}%";
                query = query.Where(e => EF.Functions.ILike(e.MiddleName, textFilter));
            }

            if (!string.IsNullOrWhiteSpace(LastName))
            {
                var textFilter = $"%{LastName.Trim().ToLower()}%";
                query = query.Where(e => EF.Functions.ILike(e.LastName, textFilter));
            }

            if (!string.IsNullOrWhiteSpace(LegalEntityName))
            {
                var textFilter = $"%{LegalEntityName.Trim().ToLower()}%";
                query = query.Where(e => EF.Functions.ILike(e.LegalEntityName, textFilter));
            }

            if (!string.IsNullOrWhiteSpace(LegalEntityUic))
            {
                var textFilter = $"%{LegalEntityUic.Trim().ToLower()}%";
                query = query.Where(e => EF.Functions.ILike(e.LegalEntityUic, textFilter));
            }

            if (SettlementId.HasValue)
            {
                query = query.Where(e => e.SettlementId == SettlementId);
            }

            if (AuthorityRiosvId.HasValue)
            {
                query = query.Where(e => e.ActivityOfferings.Any(a => a.AuthorityRiosvId == AuthorityRiosvId));
            }

            if (AuthorityBasinId.HasValue)
            {
                query = query.Where(e => e.ActivityOfferings.Any(a => a.AuthorityBasinId == AuthorityBasinId));
            }

            return base.WhereBuilder(query);
        }
    }
}
