import { FilterDto } from "../../../../infrastructure/models/filter.dto";

export class BaseNomenclatureFilterDto extends FilterDto {
  entityId: number | null;

  includeInactive: boolean | null;
  excludeAlreadyAdded: boolean = false;
}