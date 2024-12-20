import { Settlement } from "../models/settlement/settlement.model";

export class OperatorCorrespondenceDto {

    id: string;

    settlementId: number;
    settlement: Settlement;

    correspondenceAddress: string
    phone: string;
    fax: string;
    email: string;
    contactPerson: string;
    postalCode: string;

    constructor() {
        this.settlement = new Settlement();
    }
}