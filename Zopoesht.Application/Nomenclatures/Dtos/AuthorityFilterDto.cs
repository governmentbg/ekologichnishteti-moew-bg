using Zopoesht.Data.Nomenclatures;
using Zopoesht.Data.Nomenclatures.Operators;

namespace Zopoesht.Application.Nomenclatures.Dtos
{
    public class AuthorityFilterDto : BaseNomenclatureFilterDto<Authority>
    {
        public AuthorityType? Type { get; set; }

        public override IQueryable<Authority> WhereBuilder(IQueryable<Authority> query)
        {
            if (Type.HasValue)
            {
                if (Type.Value == AuthorityType.Riosv)
                {
                    query = query.Where(e => e.AuthorityType == AuthorityType.Riosv);
                }
                else if (Type.Value == AuthorityType.Bduv)
                {
                    query = query.Where(e => e.AuthorityType == AuthorityType.Bduv);
                }
            }

            return base.WhereBuilder(query);
        }
    }
}
