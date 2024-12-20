import { Activity } from "../models/activity.model";
import { SubActivity } from "../models/sub-activity.model";
import { AuthorityDto } from "./authority.dto";
import { OperatorDto } from "./operator.dto";
import { TerrainDto } from "./terrain.dto";

export class ActivityOfferingDto {
    id: number;

    activityId: number;
    activity: Activity;

    subActivityId: number;
    subActivity: SubActivity;

    operatorId: number;
    operator: OperatorDto;

    terrainId: number;
    terrain: TerrainDto;

    authorityRiosvId: number;
    authorityRiosv: AuthorityDto;

    authorityBasinId: number;
    authorityBasin: AuthorityDto;

    isInEditMode: boolean = false;
    isUsed: boolean = false;

    constructor() {
        this.activity = new Activity();
        this.subActivity = new SubActivity();
        this.operator = new OperatorDto();
        this.terrain = new TerrainDto();
        this.authorityRiosv = new AuthorityDto();
        this.authorityBasin = new AuthorityDto();
    }
}