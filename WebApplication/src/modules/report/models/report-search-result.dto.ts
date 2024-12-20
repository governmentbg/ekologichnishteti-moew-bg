import { DamageDto } from "./damage.dto";
import { ReportedDamageDto } from "./reported-damage.dto";
import { ThreatDto } from "./threat.dto";

export class ReportSearchResultDto {
  itemName: string;
  itemCount: number;
  threat: ThreatDto;
  damage: DamageDto;
  reportedDamage: ReportedDamageDto;
}