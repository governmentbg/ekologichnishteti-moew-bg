import { CommonModule } from "@angular/common";
import { Component, Input, OnInit } from "@angular/core";
import { FormsModule } from "@angular/forms";
import { TranslateModule } from "@ngx-translate/core";
import { SummaryResource } from "../../../resources/summary.resource";
import { SummaryDto } from "../../../models/summary/summary-dto";
import { SummaryTypeBasedOnApplicationOneEnLocalizaiton, SummaryTypeBasedOnApplicationOneLocalizaiton } from "../../../../../infrastructure/constants/enum-localization.const";
import { ApplicationOneType } from "../../../../application/application-one/enums/application-one-type.enum";
import { ApplicantType } from "../../../../application/application-one/enums/applicant-type.enum";

@Component({
  selector: 'summary-common-component',
  standalone: true,
  templateUrl: './summary-common.component.html',
  styleUrls: ['./summary-common.component.css'],
  imports: [
    FormsModule,
    CommonModule,
    TranslateModule
  ],
  providers: [
    SummaryResource
  ]
})
export class SummaryCommonComponent implements OnInit {

  @Input() summaries: SummaryDto[];
  @Input() isBg: boolean;

  applicationOneType = ApplicationOneType;
  summaryTypeBasedOnApplicationOneLocalizaiton = SummaryTypeBasedOnApplicationOneLocalizaiton;
  summaryTypeBasedOnApplicationOneEnLocalizaiton = SummaryTypeBasedOnApplicationOneEnLocalizaiton;

  applicantType = ApplicantType;

  constructor() {

  }

  ngOnInit(): void {

  }
}
