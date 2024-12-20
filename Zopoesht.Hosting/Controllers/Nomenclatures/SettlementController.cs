using Microsoft.AspNetCore.Mvc;
using Zopoesht.Application.Nomenclatures.Dtos;
using Zopoesht.Application.Nomenclatures.Dtos.Settlements;
using Zopoesht.Application.Nomenclatures.Interfaces;
using Zopoesht.Data.Nomenclatures.Settlements;
using Zopoesht.Hosting.Controllers.Common;

namespace Zopoesht.Hosting.Controllers.Nomenclatures
{
    public class SettlementController : BaseNomenclatureController<Settlement, SettlementMapperDto, SettlementFilterDto>
    {
        public SettlementController(INomenclatureService<Settlement> service)
            : base(service)
        {

        }
    }

    public class DistrictController : BaseNomenclatureController<District, NomenclatureMapperDto<District>, BaseNomenclatureFilterDto<District>>
    {
        public DistrictController(INomenclatureService<District> service)
            : base(service)
        {

        }
    }

    public class MunicipalityController : BaseNomenclatureController<Municipality, MunicipalityMapperDto, MunicipalityFilterDto>
    {
        public MunicipalityController(INomenclatureService<Municipality> service)
            : base(service)
        {

        }
    }
}
