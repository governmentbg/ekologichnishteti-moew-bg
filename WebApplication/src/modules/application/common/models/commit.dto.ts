import { CommitState } from "../enums/commit-state.enum";
import { ApplicationLockDto } from "./application-lock.dto";

export class CommitDto {
  id: number;
  commitState: CommitState;
  blankDate: Date;
  rootId: number;
  hasHistory: boolean | null;
  hasPermissionToEdit: boolean;
  hasPermissionToControl: boolean;

  locks: ApplicationLockDto[] = [];
}