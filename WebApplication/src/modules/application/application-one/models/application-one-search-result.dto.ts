import { CommitState } from "../../common/enums/commit-state.enum";
import { ApplicantType } from "../enums/applicant-type.enum";
import { ApplicationOneType } from "../enums/application-one-type.enum";

export class ApplicationOneSearchResultDto {
  id: number;
  rootId: number;
  registerNumber: string;
  applicationOneType: ApplicationOneType;
  applicantType: ApplicantType;
  commitState: CommitState;
  applicantName: string;
  basicName: string;
  createDate: Date;
  hasApplicationTwo: boolean;
  applicationTwoCommitState: CommitState;
}