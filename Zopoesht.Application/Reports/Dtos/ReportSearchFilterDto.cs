using Zopoesht.Application.Reports.Enums;
using Zopoesht.Data.Applications.ApplicationOne;
using Zopoesht.Data.Common.Enums;
using Zopoesht.Data.Nomenclatures;

namespace Zopoesht.Application.Reports.Dtos
{
    public class ReportSearchFilterDto
    {
        public ReportType ReportType { get; set; }

        public Authority TerritorialRange { get; set; }

        public IQueryable<ApplicationOneTerritorialRange> WhereBuilder(IQueryable<ApplicationOneTerritorialRange> query)
        {
            if (ReportType == ReportType.TerritorialRange
                && TerritorialRange != null)
            {
                query = query.Where(aotr => aotr.AuthorityId == TerritorialRange.Id);
            }

            query = query.Where(aotr => aotr.ApplicationOneBasic.ApplicationOne.CommitState != CommitState.Archived
                    && aotr.ApplicationOneBasic.ApplicationOne.CommitState != CommitState.Deleted);

            return query;
        }

        public IQueryable<ApplicationOne> WhereBuilder(IQueryable<ApplicationOne> query)
        {
            query = query.Where(a => a.CommitState != CommitState.Archived
                    && a.CommitState != CommitState.Deleted);

            return query;
        }
    }
}
