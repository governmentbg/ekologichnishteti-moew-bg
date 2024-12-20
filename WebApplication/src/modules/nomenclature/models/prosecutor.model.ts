import { ProsecutorType } from "../enums/prosecutor-type";
import { Nomenclature } from "../common/models/nomenclature.model";

export class Prosecutor extends Nomenclature {
  prosecutorType: ProsecutorType;
}