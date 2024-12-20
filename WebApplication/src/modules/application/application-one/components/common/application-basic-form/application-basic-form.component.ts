import { ChangeDetectorRef, Component, EventEmitter, Input, Output } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { TranslateModule } from '@ngx-translate/core';
import { AsyncSelectComponent } from '../../../../../../infrastructure/components/async-select/async-select.component';
import { CommonFormComponent } from '../../../../../../infrastructure/components/common-form/common-form.component';
import { SharedService } from '../../../../../../infrastructure/services/shared-service';
import { ApplicationOneDto } from '../../../models/application-one.dto';
import { CommonModule } from '@angular/common';
import { HelpTooltipComponent } from '../../../../../../infrastructure/components/help-tooltip/help-tooltip.component';
import { SvgIconComponent } from '../../../../../../infrastructure/components/svg-icon/svg-icon.component';
import { AuthorityDto } from '../../../../../nomenclature/dtos/authority.dto';
import { OperatorType } from '../../../enums/operator-type.enum';
import { ApplicantType } from '../../../enums/applicant-type.enum';
import { Activity } from '../../../../../nomenclature/models/activity.model';
import { AuthorityType } from '../../../enums/authority-type.enum';
import { ActivityOfferingDto } from '../../../../../nomenclature/dtos/activity-offering.dto';
import { OperatorInfoComponent } from '../../../../../operator/components/operator-info/operator-info.component';
import { NomenclatureDto } from '../../../../../nomenclature/common/models/nomenclature.dto';
import { ActivityOfferingDropdownDto } from '../../../../../nomenclature/dtos/activity-offering-dropdown.dto';
import { ActivityOfferingService } from '../../../../../operator/services/activity-offering.service';
import { ActivityOfferingType } from '../../../../common/enums/activity-offering-type.enum';

@Component({
  selector: 'app-application-basic-form',
  standalone: true,
  imports: [
    FormsModule,
    TranslateModule,
    AsyncSelectComponent,
    CommonModule,
    HelpTooltipComponent,
    SvgIconComponent,
    OperatorInfoComponent
  ],
  templateUrl: './application-basic-form.component.html',
  styleUrl: './application-basic-form.component.css'
})
export class ApplicationBasicFormComponent extends CommonFormComponent<ApplicationOneDto> {
  @Input() isViewMode: boolean;
  @Input() isEditMode: boolean;
  @Input() modelBasicName: string;
  @Output() modelBasicNameChange = new EventEmitter<string>();

  canAddTerritorialRange: boolean = true;
  canAddActivityOffering: boolean = true;
  canAddAffectedCountry: boolean = true;

  operatorTypes = OperatorType;
  applicantTypes = ApplicantType;

  currentOperatorId: number = 0;
  activities: Activity[] = [];
  authorityType = AuthorityType;
  activityOfferingTypes = ActivityOfferingType;

  maxTextLength1024 = 1024;

  applicationOneTerritorialRangeIds: number[] = [];
  applicationOneActivityOfferingIds: number[] = [];
  applicationOneAffectedCountryIds: number[] = [];

  unusedActivityOffering: ActivityOfferingDropdownDto[] = [];

  constructor(
    private cdr: ChangeDetectorRef,
    private activityOfferingService: ActivityOfferingService,
    public sharedService: SharedService
  ) {
    super();
  }

  isAnyChecked(): boolean {
    return this.model.applicationOneBasic.hasWaterDamage ||
      this.model.applicationOneBasic.hasSoilDamage ||
      this.model.applicationOneBasic.hasSpeciesDamage;
  }

  ngDoCheck() {
    this.cdr.detectChanges();
    this.modelBasicNameChange.next(this.modelBasicName);

    for (let i = 0; i < this.model.applicationOneBasic.territorialRanges.length; i++) {
      if (!this.model.applicationOneBasic.territorialRanges[i].id) {
        this.isValidForm.emit("INVALID");
      }
    }

    for (let i = 0; i < this.model.applicationOneBasic.affectedCountries?.length; i++) {
      if (!this.model.applicationOneBasic.affectedCountries[i].id) {
        this.isValidForm.emit("INVALID");
      }
    }

    if (this.model.applicant.applicantType === this.applicantTypes.operator
      && this.model.applicant.operator) {
      this.model.applicationOneBasic.operatorId = this.model.applicant.operator.id;
      this.model.applicationOneBasic.operator = this.model.applicant.operator;
    }

    if (this.model.applicationOneBasic.territorialRanges.length === 0) {
      this.addTerritorialRange();
    }

    this.validateTerritorialRanges();

    this.applicationOneTerritorialRangeIds = this.model.applicationOneBasic.territorialRanges
      .map(({ id }) => id)
      .filter(i => i !== undefined);

    this.validateActivityOfferings();

    this.validateAffectedCountries();

    this.applicationOneAffectedCountryIds = this.model.applicationOneBasic.affectedCountries?.map(({ id }) => id)
      .filter(i => i !== undefined);
  }

  addTerritorialRange() {
    this.model.applicationOneBasic.territorialRanges.push(new AuthorityDto());
  }

  removeTerritorialRange(index: number) {
    this.model.applicationOneBasic.territorialRanges.splice(index, 1);

    this.validateTerritorialRanges();

    if (this.model.applicationOneBasic.territorialRanges.length === 0) {
      this.addTerritorialRange();
    }

    this.canAddActivityOffering = !this.model?.applicationOneBasic?.activityOfferings?.some(a => a.operatorId === undefined);
  }

  fillTerritorialRange(): void {
    this.applicationOneTerritorialRangeIds = this.model.applicationOneBasic.territorialRanges
      .map(({ id }) => id)
      .filter(i => i !== undefined);

    this.canAddTerritorialRange = true;
  }

  validateTerritorialRanges(): void {
    for (let index = 0; index < this.model.applicationOneBasic.territorialRanges.length; index++) {
      if (this.model.applicationOneBasic.territorialRanges[index].id === 0 ||
        this.model.applicationOneBasic.territorialRanges[index].id === undefined) {
        this.canAddTerritorialRange = false;
        return;
      }
    }

    this.canAddTerritorialRange = true;
  }

  onActivityChange(index: number, dto: ActivityOfferingDropdownDto): void {
    this.model.applicationOneBasic.activityOfferings[index] = JSON.parse(JSON.stringify(dto));

    let operatorActivityOfferingIndex = this.model.applicationOneBasic.operator.activityOfferings
      .findIndex(x => x.id === dto.id);
    this.model.applicationOneBasic.operator.activityOfferings[operatorActivityOfferingIndex].isUsed = true;

    this.constructActivityOfferings();
  }

  getActivityNameWithSubActivityCode(activityOffering: ActivityOfferingDto): string {
    return `${activityOffering.subActivity.code} ${activityOffering.activity.name}`;
  }

  addActivityOffering() {
    this.model.applicationOneBasic.activityOfferings.push(new ActivityOfferingDto());

    this.constructActivityOfferings();
  }

  removeActivityOffering(index: number, dto: ActivityOfferingDto) {
    this.model.applicationOneBasic.activityOfferings.splice(index, 1);

    let isEmptyField = dto?.id === undefined ||
      dto?.id === null ||
      dto?.id <= 0;
    if (!isEmptyField) {
      let operatorActivityOfferingIndex = this.model.applicationOneBasic.operator.activityOfferings
        .findIndex(x => x.id === dto.id);
      this.model.applicationOneBasic.operator.activityOfferings[operatorActivityOfferingIndex].isUsed = false;
    }

    this.constructActivityOfferings();
  }

  validateActivityOfferings(): void {
    for (let index = 0; index < this.model.applicationOneBasic.activityOfferings.length; index++) {
      if (this.model.applicationOneBasic.activityOfferings[index].id === 0 ||
        this.model.applicationOneBasic.activityOfferings[index].id === undefined) {
        this.canAddActivityOffering = false;
        return;
      }
    }

    this.canAddActivityOffering = true;
  }

  clearActivityOfferingTypeVariables(): void {
    this.model.applicationOneBasic.activityOfferings = [];
    this.model.applicationOneBasic.operator.activityOfferings.forEach(element => {
      element.isUsed = false;
    });

    this.model.applicationOneBasic.diffuseNatureDescription = null;
    this.model.applicationOneBasic.notListedDescription = null;
  }

  onActivityOfferingTypeChange(): void {
    this.clearActivityOfferingTypeVariables();
  }

  removeActivityOfferingType(): void {
    this.model.applicationOneBasic.activityOfferingType = null;
    this.clearActivityOfferingTypeVariables();
  }

  onInternationalElementChange() {
    this.model.applicationOneBasic.culpritCountryId = null;
    this.model.applicationOneBasic.culpritCountry = null;
    this.model.applicationOneBasic.affectedCountries = [];
    this.model.applicationOneBasic.internationalElementDescription = null;

    if (this.model.applicationOneBasic.hasInternationalElement) {
      this.addAffectedCountry();
    }
  }

  addAffectedCountry() {
    this.model.applicationOneBasic.affectedCountries.push(new NomenclatureDto());
  }

  removeAffectedCountry(index: number) {
    this.model.applicationOneBasic.affectedCountries.splice(index, 1);

    this.validateAffectedCountries();

    if (this.model.applicationOneBasic.affectedCountries.length === 0) {
      this.addAffectedCountry();
    }
  }

  fillAffectedCountry(): void {
    this.applicationOneAffectedCountryIds = this.model.applicationOneBasic.affectedCountries
      .map(({ id }) => id)
      .filter(i => i !== undefined);

    this.canAddAffectedCountry = true;
  }

  validateAffectedCountries(): void {
    for (let index = 0; index < this.model.applicationOneBasic.affectedCountries?.length; index++) {
      if (this.model.applicationOneBasic.affectedCountries[index].id === 0 ||
        this.model.applicationOneBasic.affectedCountries[index].id === undefined) {
        this.canAddAffectedCountry = false;
        return;
      }
    }

    this.canAddAffectedCountry = true;
  }

  onOperatorChange(event: any) {
    this.model.applicationOneBasic.operatorId = event?.id;
    this.model.applicationOneBasic.operator = event;
    this.model.applicationOneBasic.activityOfferings = [];
    this.model.applicationOneBasic.activityOfferingType = null;
    this.model.applicationOneBasic.notListedDescription = null;
    this.model.applicationOneBasic.diffuseNatureDescription = null;
    this.activities = [];
  }

  constructActivityOfferings() {
    let usedActivityOfferingIds = this.model?.applicationOneBasic?.activityOfferings
      ?.map(({ id }) => id);

    this.unusedActivityOffering = this.model.applicationOneBasic?.operator?.activityOfferings
      ?.filter(a => !usedActivityOfferingIds
        .some(id => id === a.id))
      .map(a => JSON.parse(JSON.stringify(a)));

    this.unusedActivityOffering = this.activityOfferingService.getActivityOfferingDropdownDto(this.unusedActivityOffering);
  }
}