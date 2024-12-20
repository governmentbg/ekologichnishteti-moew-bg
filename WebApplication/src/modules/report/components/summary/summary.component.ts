import { Component, OnInit } from "@angular/core";
import { FormsModule } from "@angular/forms";
import { CommonModule } from "@angular/common";
import { TranslateModule, TranslateService } from "@ngx-translate/core";
import { SvgIconComponent } from "../../../../infrastructure/components/svg-icon/svg-icon.component";
import { SummaryResource } from "../../resources/summary.resource";
import { SummaryReportDto } from "../../models/summary/summary-result-dto";
import { LoadingIndicatorService } from "../../../../infrastructure/components/loading-indicator/services/loading-indicator.service";
import { finalize } from "rxjs";
import { SummaryCommonComponent } from "./summary-common/summary-common.component";
import { NgbModule } from "@ng-bootstrap/ng-bootstrap";
import { ValidDateDirective } from "../../../../infrastructure/directives/valid-date.directive";
import { SummaryFilterDto } from "../../services/summary.filter";

@Component({
  selector: 'summary-component',
  standalone: true,
  templateUrl: './summary.component.html',
  styleUrls: ['./summary.component.css'],
  imports: [
    FormsModule,
    CommonModule,
    TranslateModule,
    SvgIconComponent,
    SummaryCommonComponent,
    NgbModule,
    ValidDateDirective
  ],
  providers: [
    SummaryResource
  ]
})
export class SummaryComponent implements OnInit {
  summaryReportDto: SummaryReportDto;
  filter: SummaryFilterDto = new SummaryFilterDto();

  today = new Date();
  maxDate = { year: this.today.getFullYear(), month: this.today.getMonth() + 1, day: this.today.getDate() };

  constructor(private summaryResource: SummaryResource,
    private loadingIndicator: LoadingIndicatorService,
    private translate: TranslateService
  ) {

  }

  ngOnInit() {
    this.search();
  }

  public search(): void {
    this.loadingIndicator.show();

    this.summaryResource.getSummary(this.filter)
      .pipe(
        finalize(() => this.loadingIndicator.hide())
      )
      .subscribe(result => {
        this.summaryReportDto = result;
      });
  }

  public clearFilter(): void {
    this.filter.dateFrom = null;
    this.filter.dateTo = null;
  }

  changeLanguage(): void {
    this.filter.isBg = !this.filter.isBg;

    // let param = this.filter.isBg ? "bg" : "en";
    // this.translate.setDefaultLang(param);
  }

  export(): void {
    this.loadingIndicator.show();

    this.summaryResource.exportWordFile(this.filter)
      .pipe(
        finalize(() => this.loadingIndicator.hide())
      )
      .subscribe((blob: Blob) => {
        const url = window.URL.createObjectURL(blob);

        const a = document.createElement('a');
        a.href = url;
        a.download = `Summary_${new Date().toISOString().slice(0, 10)}.docx`;

        a.click();

        window.URL.revokeObjectURL(url);
      });
  }
}
