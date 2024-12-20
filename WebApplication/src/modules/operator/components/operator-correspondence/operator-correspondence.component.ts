import { Component, Input } from '@angular/core';
import { OperatorCorrespondenceDto } from '../../../nomenclature/dtos/operator-correspondence.dto';
import { TranslateModule } from '@ngx-translate/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { AsyncSelectComponent } from '../../../../infrastructure/components/async-select/async-select.component';
import { CommonFormComponent } from '../../../../infrastructure/components/common-form/common-form.component';
import { RegExps } from '../../../../infrastructure/constants/constants';

@Component({
  selector: 'operator-correspondence',
  standalone: true,
  imports: [
    TranslateModule,
    CommonModule,
    FormsModule,
    AsyncSelectComponent
  ],
  templateUrl: './operator-correspondence.component.html',
})
export class OperatorCorrespondenceComponent extends CommonFormComponent<OperatorCorrespondenceDto> {
  @Input() operatorCorrespondence: OperatorCorrespondenceDto;
  @Input() isEditMode: boolean;

  emailRegex = RegExps.EMAIL_REGEX;
  phoneRegex = RegExps.PHONE_NUMBER_REGEX;
  numberRegex = RegExps.NUMBER_REGEX;
}
