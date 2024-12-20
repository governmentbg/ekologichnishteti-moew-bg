import { CommonModule } from "@angular/common";
import { Component, Input } from "@angular/core";
import { FormsModule } from "@angular/forms";
import { TranslateModule } from "@ngx-translate/core";
import { ApplicantTypeLocalization, ApplicationOneTypeLocalization, CommitStateLocalization } from "../../../../../infrastructure/constants/enum-localization.const";
import { Router } from "@angular/router";
import { SvgIconComponent } from "../../../../../infrastructure/components/svg-icon/svg-icon.component";
import { CommitState } from "../../enums/commit-state.enum";
import { ApplicantType } from "../../../application-one/enums/applicant-type.enum";

@Component({
  selector: 'application-search-table',
  standalone: true,
  templateUrl: 'application-search-table.component.html',
  styleUrl: 'application-search-table.component.css',
  imports: [
    FormsModule,
    CommonModule,
    TranslateModule,
    SvgIconComponent
  ],
  providers: []
})
export class ApplicationSearchTableComponent {
  @Input() model: any[] = [];

  applicantTypes = ApplicantType;
  applicationTypeLocalization = ApplicationOneTypeLocalization;
  applicantTypeLocalization = ApplicantTypeLocalization;
  commitStateLocalization = CommitStateLocalization;
  commitStates = CommitState;

  constructor(
    private router: Router
  ) { }

  routeToView(id: number) {
    this.router.navigate(['/application/one/', id]);
  }
}
