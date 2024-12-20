import { AuthorityType } from "../../application/application-one/enums/authority-type.enum";
import { Nomenclature } from "../common/models/nomenclature.model";

export class Authority extends Nomenclature {
    authorityType: AuthorityType;
}