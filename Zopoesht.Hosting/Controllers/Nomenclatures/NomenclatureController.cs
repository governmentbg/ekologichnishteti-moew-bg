using Zopoesht.Application.AdministrativeExpenses.Dtos;
using Zopoesht.Application.Nomenclatures.Dtos;
using Zopoesht.Application.Nomenclatures.Interfaces;
using Zopoesht.Application.Operators.Dtos;
using Zopoesht.Data.Nomenclatures;
using Zopoesht.Data.Nomenclatures.Operators;
using Zopoesht.Data.Nomenclatures.StateAdministration;
using Zopoesht.Data.Users;
using Zopoesht.Hosting.Controllers.Common;

namespace Zopoesht.Hosting.Controllers.Nomenclatures
{
    public class AuthorityController : BaseNomenclatureController<Authority, NomenclatureMapperDto<Authority>, AuthorityFilterDto>
    {
        public AuthorityController(INomenclatureService<Authority> service)
            : base(service)
        {

        }
    }

    public class CodeController : BaseNomenclatureController<Code, NomenclatureMapperDto<Code>, CodeSearchFilter>
    {
        public CodeController(INomenclatureService<Code> service)
            : base(service)
        {

        }
    }

    public class ActivityController : BaseNomenclatureController<Activity, NomenclatureMapperDto<Activity>, ActivityFilterDto>
    {
        public ActivityController(INomenclatureService<Activity> service)
            : base(service)
        {

        }
    }

    public class SubActivityController : BaseNomenclatureController<SubActivity, NomenclatureMapperDto<SubActivity>, SubActivitySearchFilterDto>
    {
        public SubActivityController(INomenclatureService<SubActivity> service)
            : base(service)
        {

        }
    }

    public class RoleController : BaseNomenclatureController<Role, NomenclatureMapperDto<Role>, BaseNomenclatureFilterDto<Role>>
    {
        public RoleController(INomenclatureService<Role> service)
            : base(service)
        {

        }
    }

    public class OperatorNomenclatureController : BaseNomenclatureController<Operator, OperatorNomenclatureMapperDto, BaseNomenclatureFilterDto<Operator>>
    {
        public OperatorNomenclatureController(INomenclatureService<Operator> service)
            : base(service)
        {

        }
    }

    public class TerrainNomenclatureController : BaseNomenclatureController<Terrain, TerrainNomenclatureMapperDto, TerrainFilterDto>
    {
        public TerrainNomenclatureController(INomenclatureService<Terrain> service)
            : base(service)
        {

        }
    }

    public class CountryController : BaseNomenclatureController<Country, NomenclatureMapperDto<Country>, BaseNomenclatureFilterDto<Country>>
    {
        public CountryController(INomenclatureService<Country> service)
            :base(service)
        {

        }
    }

    public class ProsecutorController : BaseNomenclatureController<Prosecutor, NomenclatureMapperDto<Prosecutor>, BaseNomenclatureFilterDto<Prosecutor>>
    {
        public ProsecutorController(INomenclatureService<Prosecutor> service)
            : base(service)
        {

        }
    }

    public class MinistryOfInteriorController : BaseNomenclatureController<MinistryOfInterior, NomenclatureMapperDto<MinistryOfInterior>, BaseNomenclatureFilterDto<MinistryOfInterior>>
    {
        public MinistryOfInteriorController(INomenclatureService<MinistryOfInterior> service)
            : base(service)
        {

        }
    }

    public class AdministrativeCourtController : BaseNomenclatureController<AdministrativeCourt, AdministrativeCourtDto, BaseNomenclatureFilterDto<AdministrativeCourt>>
    {
        public AdministrativeCourtController(INomenclatureService<AdministrativeCourt> service)
            : base(service)
        {

        }
    }

    public class YearController : BaseNomenclatureController<Year, YearDto, BaseNomenclatureFilterDto<Year>>
    {
        public YearController(INomenclatureService<Year> service)
            : base(service)
        {

        }
    }

    public class PeriodNomenclatureController : BaseNomenclatureController<Period, NomenclatureMapperDto<Period>, PeriodFilterDto>
    {
        public PeriodNomenclatureController(INomenclatureService<Period> service)
            : base(service)
        {

        }
    }
}