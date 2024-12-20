import { Component, EventEmitter, Input, Output } from '@angular/core';
import { TranslateModule } from '@ngx-translate/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { AsyncSelectComponent } from '../../../../infrastructure/components/async-select/async-select.component';
import { OperatorDto } from '../../../nomenclature/dtos/operator.dto';
import { OperatorType } from '../../../application/application-one/enums/operator-type.enum';
import { ApplicantType } from '../../../application/application-one/enums/applicant-type.enum';
import { CommonFormComponent } from '../../../../infrastructure/components/common-form/common-form.component';

@Component({
  selector: 'operator-info',
  standalone: true,
  imports: [
    TranslateModule,
    CommonModule,
    FormsModule,
    AsyncSelectComponent
  ],
  templateUrl: './operator-info.component.html',
})
export class OperatorInfoComponent extends CommonFormComponent<OperatorDto> {
  @Input() model: OperatorDto;
  @Input() applicantType: ApplicantType;

  @Input() isEditMode: boolean;
  @Input() isDisabled: boolean;

  @Output() operatorChangeEvent: EventEmitter<any> = new EventEmitter();

  operatorTypes = OperatorType;
  applicantTypes = ApplicantType;
}
