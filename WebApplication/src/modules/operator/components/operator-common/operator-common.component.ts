import { CommonModule } from '@angular/common';
import { Component, EventEmitter, Input, Output } from '@angular/core';
import { OperatorResource } from '../../resources/operator.resource';
import { TranslateModule } from '@ngx-translate/core';
import { OperatorDto } from '../../../nomenclature/dtos/operator.dto';
import { FormsModule } from '@angular/forms';
import { AsyncSelectComponent } from '../../../../infrastructure/components/async-select/async-select.component';
import { OperatorCorrespondenceComponent } from '../operator-correspondence/operator-correspondence.component';
import { CollapsableLabelComponent } from '../../../../infrastructure/components/collapsable-label/collapsable-label.component';
import { OperatorActivitiesComponent } from '../operator-activities/operator-activities.component';
import { OperatorTerrainsComponent } from '../operator-terrains/operator-terrains.component';
import { OperatorType } from '../../../application/application-one/enums/operator-type.enum';
import { OperatorTypeLocalization } from '../../../../infrastructure/constants/enum-localization.const';
import { CommonFormComponent } from '../../../../infrastructure/components/common-form/common-form.component';
import { RegExps } from '../../../../infrastructure/constants/constants';

@Component({
  selector: 'operator-common',
  standalone: true,
  imports: [
    TranslateModule,
    CommonModule,
    FormsModule,
    AsyncSelectComponent,
    OperatorCorrespondenceComponent,
    OperatorActivitiesComponent,
    OperatorTerrainsComponent,
    CollapsableLabelComponent
  ],
  providers: [
    OperatorResource
  ],
  templateUrl: './operator-common.component.html',
  styleUrl: './operator-common.component.css'
})
export class OperatorCommonComponent extends CommonFormComponent<OperatorDto> {
  @Input() operator = new OperatorDto();
  @Input() isEditMode: boolean = false;

  @Output() canSubmit: EventEmitter<boolean> = new EventEmitter();

  operatorTypes = OperatorType;
  operatorTypeLocalization = OperatorTypeLocalization;

  isOperatorCorrespondenceFormValid: boolean = false;

  numberRegex = RegExps.NUMBER_REGEX;

  changeFormValidStatus(status: string): void {
    this.isOperatorCorrespondenceFormValid = status === 'VALID';
  }

  areOperatorSettlementsValid(): boolean {
    return this.operator?.settlementId > 0 &&
      this.operator?.operatorCorrespondence?.settlementId > 0;
  }

  ngDoCheck(): void {
    let operatorCommonForm = this.form?.status === "VALID";
    this.canSubmit.emit(operatorCommonForm && this.isOperatorCorrespondenceFormValid && this.areOperatorSettlementsValid());
  }
}
