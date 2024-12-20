import { Component } from "@angular/core";
import { AsyncSelectComponent } from "../../../../infrastructure/components/async-select/async-select.component";
import { ReportSearchFilter } from "../../services/report-search.filter";
import { BaseSearchComponent } from "../../../../infrastructure/components/search-component/base-search.component";
import { ReportResource } from "../../resources/report.resource";
import { LoadingIndicatorService } from "../../../../infrastructure/components/loading-indicator/services/loading-indicator.service";
import { FormsModule } from "@angular/forms";
import { ReportSearchResultDto } from "../../models/report-search-result.dto";
import { CommonModule } from "@angular/common";
import { TranslateModule } from "@ngx-translate/core";
import { SvgIconComponent } from "../../../../infrastructure/components/svg-icon/svg-icon.component";
import { ReportType } from "../../enums/report-type.enum";
import { ReportTypeLocalization } from "../../../../infrastructure/constants/enum-localization.const";
import { ApplicationOneResource } from "../../../application/application-one/resources/application-one.resource";
import { finalize } from "rxjs";

@Component({
  selector: 'report-search',
  standalone: true,
  templateUrl: './report-search.component.html',
  styleUrl: './report-search.component.css',
  imports: [
    FormsModule,
    CommonModule,
    TranslateModule,
    SvgIconComponent,
    AsyncSelectComponent
  ],
  providers: [
    ApplicationOneResource,
    ReportResource,
    ReportSearchFilter
  ]
})
export class ReportSearchComponent extends BaseSearchComponent<ReportSearchResultDto> {

  reportType = ReportType;
  reportTypeLocalization = ReportTypeLocalization;

  constructor(
    private applicationOneResource: ApplicationOneResource,
    public override filter: ReportSearchFilter,
    protected override resource: ReportResource,
    protected override loadingIndicator: LoadingIndicatorService
  ) {
    super(filter, resource, loadingIndicator);
  }

  ngOnInit(): void {
  }

  onReportTypeChange(): void {
    this.search();
  }

  clearFilter(): void {
    this.filter.clear();
    this.modelCounts = 0;
  }

  export(): void {
    this.loadingIndicator.show();

    this.applicationOneResource.exportExcelFile(this.filter)
      .pipe(
        finalize(() => this.loadingIndicator.hide())
      )
      .subscribe((blob: Blob) => {
        const url = window.URL.createObjectURL(blob);

        const a = document.createElement('a');
        a.href = url;
        a.download = `Report_${new Date().toISOString().slice(0, 10)}.xlsx`;

        a.click();

        window.URL.revokeObjectURL(url);
      });
  }
}