import { BaseNomenclatureFilterDto } from "../common/models/base-nomenclature-filter.dto";

export class SubActivitySearchFilter extends BaseNomenclatureFilterDto {
  code: string;
  name: string;
  activityId: number;
}