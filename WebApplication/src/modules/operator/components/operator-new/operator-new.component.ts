import { Component } from '@angular/core';
import { OperatorCommonComponent } from '../operator-common/operator-common.component';
import { TranslateModule } from '@ngx-translate/core';
import { CommonModule } from '@angular/common';
import { OperatorDto } from '../../../nomenclature/dtos/operator.dto';
import { OperatorResource } from '../../resources/operator.resource';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'operator-new',
  standalone: true,
  imports: [
    OperatorCommonComponent,
    TranslateModule,
    CommonModule
  ],
  providers: [
    OperatorResource
  ],
  templateUrl: './operator-new.component.html'
})
export class OperatorNewComponent {
  operator: OperatorDto = new OperatorDto();

  isEditMode: boolean = true;
  canSubmit: boolean = false;

  constructor(
    private resource: OperatorResource,
    private router: Router,
    private toastrService: ToastrService
  ) { }

  addOperator() {
    this.resource.add(this.operator)
      .subscribe(() => {
        this.toastrService.success("Успешно добавяне на оператор!");
        this.router.navigate(['/operator/search']);
      });
  }
}
