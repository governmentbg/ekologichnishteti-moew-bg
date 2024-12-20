import { Component } from "@angular/core";
import { ActivatedRoute, Router } from "@angular/router";
import { TranslateModule } from "@ngx-translate/core";
import { CommonModule } from "@angular/common";
import { catchError, finalize } from "rxjs";
import { LoadingIndicatorService } from "../../../../infrastructure/components/loading-indicator/services/loading-indicator.service";
import { ToastrService } from "ngx-toastr";
import { OperatorCommonComponent } from "../operator-common/operator-common.component";
import { OperatorResource } from "../../resources/operator.resource";
import { OperatorDto } from "../../../nomenclature/dtos/operator.dto";

@Component({
  selector: 'operator-view',
  standalone: true,
  templateUrl: 'operator-view.component.html',
  styleUrls: [],
  imports: [
    OperatorCommonComponent,
    TranslateModule,
    CommonModule
  ],
  providers: [
    OperatorResource
  ]
})

export class OperatorViewComponent {
  operator: OperatorDto = new OperatorDto();
  private originalModel = new OperatorDto();

  isEditMode: boolean = false;
  canSubmit: boolean = false;

  constructor(
    private route: ActivatedRoute,
    private resource: OperatorResource,
    private router: Router,
    private loadingIndicator: LoadingIndicatorService,
    private toastrService: ToastrService
  ) { }

  ngOnInit() {
    const oepratorId = this.route.snapshot.params['id'];
    this.getUserById(oepratorId);
  }

  getUserById(id: number) {
    this.resource.getById(id)
      .pipe(
        finalize(() => this.loadingIndicator.hide()),
        catchError(() => this.router.navigate(['/oeprator/search']))
      )
      .subscribe(
        (data: OperatorDto) => {
          this.operator = data;
        }
      );
  }

  enableEditing() {
    this.isEditMode = true;
    this.originalModel = JSON.parse(JSON.stringify(this.operator));
  }

  editOperator() {
    this.loadingIndicator.show();

    this.resource.update(this.operator)
      .pipe(
        finalize(() => { this.loadingIndicator.hide() })
      )
      .subscribe(() => {
        this.isEditMode = false;
        this.toastrService.success("Успешно редактиране на оператор!");
      });
  }

  cancelEdit() {
    this.operator = JSON.parse(JSON.stringify(this.originalModel));
    this.finishEdit();
  }

  private finishEdit(): void {
    this.originalModel = null;
    this.isEditMode = false;
  }

  changeFormValidStatus(status: string): void {
    this.canSubmit = status === "VALID";
  }
}