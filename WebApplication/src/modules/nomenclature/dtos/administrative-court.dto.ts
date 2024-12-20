import { AdministrativeCourtType } from "../../application/application-two/enums/administrative-court-type.enum";
import { NomenclatureDto } from "../common/models/nomenclature.dto";

export class AdministrativeCourtDto extends NomenclatureDto {
  administrativeCourtType: AdministrativeCourtType;
}