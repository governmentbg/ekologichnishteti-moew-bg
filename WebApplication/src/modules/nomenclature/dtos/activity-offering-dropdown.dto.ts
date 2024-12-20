import { SubActivity } from "../models/sub-activity.model";
import { AuthorityDto } from "./authority.dto";
import { TerrainDto } from "./terrain.dto";

export class ActivityOfferingDropdownDto {
  id: number;
  name: string;
  subActivityId: number;
  subActivity: SubActivity;
  authorityRiosvId: number;
  authorityRiosv: AuthorityDto;
  authorityBasinId: number;
  authorityBasin: AuthorityDto;
  terrainId: number;
  terrain: TerrainDto;
}