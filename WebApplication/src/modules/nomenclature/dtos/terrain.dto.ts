import { District } from "../models/settlement/district.model";
import { Municipality } from "../models/settlement/municipality.model";
import { Settlement } from "../models/settlement/settlement.model";
import { OperatorDto } from "./operator.dto";

export class TerrainDto {
    id: number;
    name: string;

    operatorId: number;
    operator: OperatorDto;

    settlementId: number;
    settlement: Settlement;

    districtId: number;
    district: District;

    municipalityId: number;
    municipality: Municipality;

    address: string;

    isInEditMode: boolean = false;
}