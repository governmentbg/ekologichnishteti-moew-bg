import { NomenclatureCode } from "../common/models/nomenclature-code.model";
import { Activity } from "./activity.model";

export class SubActivity extends NomenclatureCode {
    activityId: number;
    activity: Activity;
}