import { AuthorityType } from "../../application/application-one/enums/authority-type.enum";
import { NomenclatureDto } from "../common/models/nomenclature.dto";

export class AuthorityDto extends NomenclatureDto {
    authorityType: AuthorityType;
}