import { MortgageType } from "../../enums/mortgage-type.enum";

export class MortgageDto {
  mortgageType: MortgageType;
  mortgageNumber: string;
  mortgageDate: Date;
  mortgageDescription: string;
  value: number;
  additionalInformation: string;
}