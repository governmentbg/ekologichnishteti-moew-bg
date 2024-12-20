
import { ChangeDetectorRef, Component, EventEmitter, Input, Output } from '@angular/core';
import { ApplicationTwoDto } from '../../models/application-two.dto';
import { FormsModule } from '@angular/forms';
import { TranslateModule } from '@ngx-translate/core';
import { AsyncSelectComponent } from '../../../../../infrastructure/components/async-select/async-select.component';
import { CommonModule } from '@angular/common';
import { SvgIconComponent } from '../../../../../infrastructure/components/svg-icon/svg-icon.component';
import { CommonFormComponent } from '../../../../../infrastructure/components/common-form/common-form.component';
import { SharedService } from '../../../../../infrastructure/services/shared-service';
import { CaseStatus } from '../../enums/case-status.enum';
import { ApplicationTwoCommitStateLocalization, CaseStatusLocalization, MortgageTypeLocalization, PaymentSourceLocalization, ProceedingTypeLocalization } from '../../../../../infrastructure/constants/enum-localization.const';
import { CommitState } from '../../../common/enums/commit-state.enum';
import { PaymentSource } from '../../enums/payment-source.enum';
import { ProceedingType } from '../../enums/proceeding-type.enum';
import { ApplicantType } from '../../../application-one/enums/applicant-type.enum';
import { ApplicationOneType } from '../../../application-one/enums/application-one-type.enum';
import { AdministrativeCourtType } from '../../enums/administrative-court-type.enum';
import { IndividualDto } from '../../../application-one/models/individual.dto';
import { LegalEntityDto } from '../../../application-one/models/legal-entity.dto';
import { ApplicationTwoProsecutorDto } from '../../models/application-two-prosecutor.dto';
import { ApplicationTwoMinistryOfInteriorDto } from '../../models/applicatio-two-ministry-of-interior.dto';
import { ApplicationTwoAdministrativeCourtDto } from '../../models/application-two-administrative-court.dto';
import { CodeDto } from '../../../../nomenclature/dtos/code.dto';
import { ApplicationTwoAuhtority } from '../../models/application-two-authority.dto';
import { HelpTooltipComponent } from '../../../../../infrastructure/components/help-tooltip/help-tooltip.component';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { ValidDateDirective } from '../../../../../infrastructure/directives/valid-date.directive';
import { ApplicationFileFormComponent } from '../../../common/components/application-file-form/application-file-from.component';
import { PartPanelComponent } from '../../../../../infrastructure/components/part-panel/part-panel.component';
import { ActivityOfferingDto } from '../../../../nomenclature/dtos/activity-offering.dto';
import { Activity } from '../../../../nomenclature/models/activity.model';
import { OperatorInfoComponent } from '../../../../operator/components/operator-info/operator-info.component';
import { InsurancePolicyDto } from '../../models/financial-assurance/insurance-policy.dto';
import { MortgageDto } from '../../models/financial-assurance/mortgage-dto';
import { StakeDto } from '../../models/financial-assurance/stake-dto';
import { BankGuaranteeDto } from '../../models/financial-assurance/bank-guarantee.dto';
import { MortgageType } from '../../enums/mortgage-type.enum';
import { ActivityOfferingDropdownDto } from '../../../../nomenclature/dtos/activity-offering-dropdown.dto';
import { ActivityOfferingService } from '../../../../operator/services/activity-offering.service';
import { ActivityOfferingType } from '../../../common/enums/activity-offering-type.enum';
import { ApplicationAuthorityDto } from '../../../common/models/application-authority.dto';
@Component({
  selector: 'app-application-two-common',
  standalone: true,
  imports: [
    PartPanelComponent,
    FormsModule,
    TranslateModule,
    AsyncSelectComponent,
    CommonModule,
    NgbModule,
    SvgIconComponent,
    HelpTooltipComponent,
    ValidDateDirective,
    ApplicationFileFormComponent,
    OperatorInfoComponent
  ],
  templateUrl: './application-two-common.component.html',
  styleUrl: './application-two-common.component.css'
})
export class ApplicationTwoCommonComponent extends CommonFormComponent<ApplicationTwoDto> {

  @Input() isViewMode: boolean;
  @Input() isEditMode: boolean;
  @Input() isFileFormDisplayed: boolean = false;
  @Input() model: ApplicationTwoDto;

  @Output() canSubmit: EventEmitter<boolean> = new EventEmitter();
  @Output() areCodesValid: EventEmitter<boolean> = new EventEmitter();
  @Output() areActivityOfferingsValid: EventEmitter<boolean> = new EventEmitter();

  isDisabled: boolean = true;

  today = new Date();
  maxDate = { year: this.today.getFullYear(), month: this.today.getMonth() + 1, day: this.today.getDate() };
  insuranceMinEndDate: any;
  bankGuaranteeMinEndDate: any;

  maxTextLength1024 = 1024;
  minAmountValue = 0;

  canAddCode: boolean = true;
  canAddAuthority: boolean = true;
  canAddProsecutor: boolean = true;
  canAddMinistryOfInterior: boolean = true;
  canAddAdministrativeCourt: boolean = true;
  canAddActivityOffering: boolean = true;

  caseStatus = CaseStatus;
  caseStatusLocalization = CaseStatusLocalization;

  commitState = CommitState;
  commitStateLocalization = ApplicationTwoCommitStateLocalization;

  paymentSource = PaymentSource;
  paymentSourceLocalization = PaymentSourceLocalization;

  proceedingType = ProceedingType;
  proceedingTypeLocalization = ProceedingTypeLocalization;

  mortgageType = MortgageType;
  mortgageTypeLocalization = MortgageTypeLocalization;

  applicantTypes = ApplicantType;
  applicationOneTypes = ApplicationOneType;
  administrativeCourtTypes = AdministrativeCourtType;
  activityOfferingTypes = ActivityOfferingType;

  activities: Activity[] = [];
  activityIds: number[] = [];
  usedActivityIds: number[] = [];
  applicationTwoAuthorityIds: number[] = [];
  applicationTwoCodeIds: number[] = [];
  applicationTwoProsecutorsIds: number[] = [];
  applicationTwoMinistryOfInteriorsIds: number[] = [];
  applicationTwoAdministrativeCourtsIds: number[] = [];

  unusedActivityOffering: ActivityOfferingDropdownDto[] = [];

  constructor(
    private cdr: ChangeDetectorRef,
    private activityOfferingService: ActivityOfferingService,
    public sharedService: SharedService
  ) {
    super();
  }

  ngOnInit(): void {
    if (!this.isViewMode) {
      this.addCode();

      if (this.model.applicationTwoAuthorities?.length === 0) {
        this.addAuthority();
      }
    }
  }

  ngDoCheck() {
    this.cdr.detectChanges();

    this.canAddCode = !this.model?.codes?.some(c => c.id === undefined || c.id === null);
    this.areCodesValid.emit(this.canAddCode);

    this.applicationTwoCodeIds = this.model.codes
      .map(({ id }) => id)
      .filter(i => i !== undefined);

    this.areActivityOfferingsValid.emit(
      !this.model.applicationTwoActivityOfferings
        .map(({ activity }) => activity)
        .some(i => i.id === 0 || i.id === undefined)
    );

    this.applicationTwoAuthorityIds = this.model.applicationTwoAuthorities
      .map(({ authorityId }) => authorityId)
      .filter(i => i !== undefined);

    this.applicationTwoProsecutorsIds = this.model.applicationTwoProsecutors
      .map(({ prosecutorId }) => prosecutorId)
      .filter(i => i !== undefined);

    this.applicationTwoMinistryOfInteriorsIds = this.model.applicationTwoMinistryOfInteriors
      .map(({ ministryOfInteriorId }) => ministryOfInteriorId)
      .filter(i => i !== undefined);

    this.applicationTwoAdministrativeCourtsIds = this.model.applicationTwoAdministrativeCourts
      .map(({ administrativeCourtId }) => administrativeCourtId)
      .filter(i => i !== undefined);

    this.validateAdministrativeCourt();

    this.canAddActivityOffering = !this.model?.applicationTwoActivityOfferings?.some(a => a.operatorId === undefined);
  }

  changeFormValidStatus(status: string): void {
    this.canSubmit.emit(status === 'INVALID' ? false : true);
  }

  isAnyChecked(): boolean {
    return this.model.hasWaterDamage ||
      this.model.hasSoilDamage ||
      this.model.hasSpeciesDamage;
  }

  onApplicantTypeChange(selectedType: ApplicantType): void {
    this.model.applicant.clearAll;

    if (selectedType === this.applicantTypes.individual
      || selectedType === this.applicantTypes.legalEntity
      || selectedType === this.applicantTypes.nonGovernmentalOrganization) {

      if (selectedType === this.applicantTypes.individual) {
        this.model.applicant.individual = new IndividualDto();
      }
      else if (selectedType === this.applicantTypes.legalEntity
        || selectedType === this.applicantTypes.nonGovernmentalOrganization) {
        this.model.applicant.legalEntity = new LegalEntityDto();

      }
    }
  }

  onOperatorChange(event: any) {
    this.model.operatorId = event?.id;
    this.model.operator = event;
    this.model.applicationTwoActivityOfferings = [];
    this.model.activityOfferingType = null;
    this.model.notListedDescription = null;
    this.model.diffuseNatureDescription = null;
    this.activities = [];
  }

  onProceedingTypeChange(selectedType: ProceedingType): void {
    this.model.applicationTwoAdministrativeCourts = [];
    this.model.applicationTwoProsecutors = [];
    this.model.applicationTwoMinistryOfInteriors = [];

    if (selectedType === this.proceedingType.preTrial) {
      this.addProsecutor();
      this.addMinistryOfInterior();
    }
    else if (selectedType === this.proceedingType.legal) {
      this.addAdministrativeCourt();
    }
    else if (selectedType === this.proceedingType.both) {
      this.addProsecutor();
      this.addMinistryOfInterior();
      this.addAdministrativeCourt();
    }
  }

  onHasPreventionProcedureResultChange(): void {
    if (this.model.hasPreventionProcedureResult) {
      this.model.preventionProcedureResult = null;
    }
  }

  onHasRemovalProcedureResultChange(): void {
    if (this.model.hasRemovalProcedureResult) {
      this.model.removalProcedureResult = null;
    }
  }

  onHasUnreimbursedExpenseChange(): void {
    if (this.model.hasUnreimbursedExpense) {
      this.model.unreimbursedExpenseValue = null;
      this.model.unreimbursedExpense = null;
    }
  }

  onProceedingInfoAbsenceChange(): void {
    if (this.model.proceedingInfoAbsence) {
      this.model.proceedingType = null;
      this.model.applicationTwoProsecutors = [];
      this.model.applicationTwoMinistryOfInteriors = [];
      this.model.applicationTwoAdministrativeCourts = [];
    }
  }

  onCaseStatusChange(): void {
    if (this.model.caseStatus !== CaseStatus.completed) {
      this.model.preventionOrRemovalProcedureFinishDate = null;
      this.model.preventionOrRemovalProcedureFinishInformation = null;
    }
  }

  onPaymentSourceChange(): void {
    if (this.model.paymentSource !== PaymentSource.other) {
      this.model.otherPaymentSource = null;
    }
  }

  displayFileForm(): void {
    this.isFileFormDisplayed = true;
  }

  onActivityChange(index: number, dto: ActivityOfferingDropdownDto): void {
    this.model.applicationTwoActivityOfferings[index] = JSON.parse(JSON.stringify(dto));

    let operatorActivityOfferingIndex = this.model.operator.activityOfferings
      .findIndex(x => x.id === dto.id);
    this.model.operator.activityOfferings[operatorActivityOfferingIndex].isUsed = true;

    this.constructActivityOfferings();
  }

  getActivityNameWithSubActivityCode(activityOffering: ActivityOfferingDto): string {
    return `${activityOffering.subActivity.code} ${activityOffering.activity.name}`;
  }

  addActivityOffering() {
    this.model.applicationTwoActivityOfferings.push(new ActivityOfferingDto());

    this.constructActivityOfferings();
  }

  removeActivityOffering(index: number, dto: ActivityOfferingDto) {
    this.model.applicationTwoActivityOfferings.splice(index, 1);

    let isEmptyField = dto?.id === undefined ||
      dto?.id === null ||
      dto?.id <= 0;
    if (!isEmptyField) {
      let operatorActivityOfferingIndex = this.model.operator.activityOfferings
        .findIndex(x => x.id === dto.id);
      this.model.operator.activityOfferings[operatorActivityOfferingIndex].isUsed = false;
    }

    this.constructActivityOfferings();
  }

  validateActivityOfferings(): void {
    for (let index = 0; index < this.model.applicationTwoActivityOfferings.length; index++) {
      if (this.model.applicationTwoActivityOfferings[index].id === 0 ||
        this.model.applicationTwoActivityOfferings[index].id === undefined) {
        this.canAddActivityOffering = false;
        return;
      }
    }

    this.canAddActivityOffering = true;
  }

  constructActivityOfferings() {
    let usedActivityOfferingIds = this.model?.applicationTwoActivityOfferings
      ?.map(({ id }) => id);

    this.unusedActivityOffering = this.model.operator?.activityOfferings
      ?.filter(a => !usedActivityOfferingIds
        .some(id => id === a.id))
      .map(a => JSON.parse(JSON.stringify(a)));

    this.unusedActivityOffering = this.activityOfferingService.getActivityOfferingDropdownDto(this.unusedActivityOffering);
  }

  clearActivityOfferingTypeVariables(): void {
    this.model.applicationTwoActivityOfferings = [];
    this.model.operator.activityOfferings.forEach(element => {
      element.isUsed = false;
    });

    this.model.diffuseNatureDescription = null;
    this.model.notListedDescription = null;
  }

  onActivityOfferingTypeChange(): void {
    this.clearActivityOfferingTypeVariables();
  }

  removeActivityOfferingType(): void {
    this.model.activityOfferingType = null;
    this.clearActivityOfferingTypeVariables();
  }

  fillProsecutor(event: any, i: number): void {
    this.model.applicationTwoProsecutors[i].prosecutorId = event.id;

    this.canAddProsecutor = true;
  }

  validateProsecutors(): void {
    for (let index = 0; index < this.model.applicationTwoProsecutors.length; index++) {
      if (this.model.applicationTwoProsecutors[index].prosecutorId === 0 ||
        this.model.applicationTwoProsecutors[index].prosecutorId === undefined) {
        this.canAddProsecutor = false;
        return;
      }
    }

    this.canAddProsecutor = true;
  }

  addProsecutor(): void {
    if (this.model.applicationTwoProsecutors === null || this.model.applicationTwoProsecutors === undefined) {
      this.model.applicationTwoProsecutors = [];
    }

    this.model.applicationTwoProsecutors.push(new ApplicationTwoProsecutorDto());
    this.canAddProsecutor = false;
  }

  removeProsecutor(index: number): void {
    this.model.applicationTwoProsecutors.splice(index, 1);

    this.validateProsecutors();

    if (!this.canRemoveProsecutorOrMinistryOfInteriorsOrAdministrativeCourt()) {
      this.addProsecutor();
    }
  }

  fillMinistryOfInterior(event: any, i: number): void {
    this.model.applicationTwoMinistryOfInteriors[i].ministryOfInteriorId = event.id;

    this.canAddMinistryOfInterior = true;
  }

  validateMinistryOfInterior(): void {
    for (let index = 0; index < this.model.applicationTwoMinistryOfInteriors.length; index++) {
      if (this.model.applicationTwoMinistryOfInteriors[index].ministryOfInteriorId === 0 ||
        this.model.applicationTwoMinistryOfInteriors[index].ministryOfInteriorId === undefined) {
        this.canAddMinistryOfInterior = false;
        return;
      }
    }

    this.canAddMinistryOfInterior = true;
  }

  addMinistryOfInterior(): void {
    if (this.model.applicationTwoMinistryOfInteriors === null || this.model.applicationTwoMinistryOfInteriors === undefined) {
      this.model.applicationTwoMinistryOfInteriors = [];
    }

    this.model.applicationTwoMinistryOfInteriors.push(new ApplicationTwoMinistryOfInteriorDto());
    this.canAddMinistryOfInterior = false;
  }

  removeMinistryOfInterior(index: number): void {
    this.model.applicationTwoMinistryOfInteriors.splice(index, 1);

    this.validateMinistryOfInterior();

    if (!this.canRemoveProsecutorOrMinistryOfInteriorsOrAdministrativeCourt()) {
      this.addMinistryOfInterior();
    }
  }

  fillAdministrativeCourt(event: any, i: number): void {
    this.model.applicationTwoAdministrativeCourts[i].administrativeCourtId = event.id;

    this.canAddAdministrativeCourt = true;
  }

  validateAdministrativeCourt(): void {
    for (let index = 0; index < this.model.applicationTwoAdministrativeCourts.length; index++) {
      if (this.model.applicationTwoAdministrativeCourts[index].administrativeCourt?.administrativeCourtType === this.administrativeCourtTypes.supereme
        && this.model.applicationTwoAdministrativeCourts.length === 1
      ) {
        this.addAdministrativeCourt();
        return;
      }
      else if (this.model.applicationTwoAdministrativeCourts[index].administrativeCourtId === 0 ||
        this.model.applicationTwoAdministrativeCourts[index].administrativeCourtId === undefined) {
        this.canAddAdministrativeCourt = false;
        return;
      }
    }

    this.canAddAdministrativeCourt = true;
  }

  addAdministrativeCourt(): void {
    if (this.model.applicationTwoAdministrativeCourts === null || this.model.applicationTwoAdministrativeCourts === undefined) {
      this.model.applicationTwoAdministrativeCourts = [];
    }

    this.model.applicationTwoAdministrativeCourts.push(new ApplicationTwoAdministrativeCourtDto());
    this.canAddAdministrativeCourt = false;
  }

  removeAdministrativeCourt(index: number): void {
    this.model.applicationTwoAdministrativeCourts.splice(index, 1);

    if (!this.canRemoveProsecutorOrMinistryOfInteriorsOrAdministrativeCourt()) {
      this.addAdministrativeCourt();
    }
  }


  addCode(): void {
    if (this.model.codes === null || this.model.codes === undefined) {
      this.model.codes = [];
    }

    this.model.codes.push(new CodeDto());
  }

  removeCode(index: number): void {
    this.model.codes.splice(index, 1);
  }

  fillAuthority(event: any, i: number): void {
    this.model.applicationTwoAuthorities[i].authorityId = event.id;

    this.canAddAuthority = true;
  }

  validateAuthorities(): void {
    for (let index = 0; index < this.model.applicationTwoAuthorities.length; index++) {
      if (this.model.applicationTwoAuthorities[index].authorityId === 0 ||
        this.model.applicationTwoAuthorities[index].authorityId === undefined) {
        this.canAddAuthority = false;
        return;
      }
    }

    this.canAddAuthority = true;
  }

  addAuthority(): void {
    this.model.applicationTwoAuthorities.push(new ApplicationAuthorityDto());
    this.canAddAuthority = false;
  }

  removeAuthority(index: number): void {
    this.model.applicationTwoAuthorities.splice(index, 1);

    if (this.model.applicationTwoAuthorities.length === 0) {
      this.addAuthority();
    }

    this.validateAuthorities();
  }

  canRemoveProsecutorOrMinistryOfInteriorsOrAdministrativeCourt(): boolean {
    if (this.model.proceedingType === ProceedingType.preTrial
      && this.model.applicationTwoProsecutors.length === 0
      && this.model.applicationTwoMinistryOfInteriors.length === 0
    ) {
      return false;
    }
    else if (this.model.proceedingType === ProceedingType.legal
      && this.model.applicationTwoAdministrativeCourts.length === 0
    ) {
      return false;
    }
    else if (this.model.proceedingType === ProceedingType.both) {
      if (this.model.applicationTwoAdministrativeCourts.length > 0
        && (this.model.applicationTwoProsecutors.length > 0 || this.model.applicationTwoMinistryOfInteriors.length > 0)) {
        return true;
      }
      else {
        return false;
      }
    }

    return true;
  }

  onHasInsurancePolicyChange(): void {
    this.model.insurancePolicyId = null;
    this.model.insurancePolicy = this.model.hasInsurancePolicy ?
      new InsurancePolicyDto() :
      null;
  }

  onHasBankGuaranteeChange(): void {
    this.model.bankGuaranteeId = null;
    this.model.bankGuarantee = this.model.hasBankGuarantee ?
      new BankGuaranteeDto() :
      null;
  }

  onHasMortgageChange(): void {
    this.model.mortgageId = null;
    this.model.mortgage = this.model.hasMortgage ?
      new MortgageDto() :
      null;
  }

  onHasStakeChange(): void {
    this.model.stakeId = null;
    this.model.stake = this.model.hasStake ?
      new StakeDto() :
      null;
  }

  onHasNoCollateralChange(): void {
    this.model.noCollateralAdditionalInformation = null;

    if (this.model.hasNoCollateral) {
      this.model.hasInsurancePolicy = false;
      this.onHasInsurancePolicyChange();

      this.model.hasBankGuarantee = false;
      this.onHasBankGuaranteeChange();

      this.model.hasMortgage = false;
      this.onHasMortgageChange();

      this.model.hasStake = false;
      this.onHasStakeChange();
    }
  }

  onInsuranceDateChange(): void {
    if (this.model.insurancePolicy.insuranceStart !== undefined &&
      this.model.insurancePolicy.insuranceStart !== null) {
      const startDate = new Date(this.model.insurancePolicy.insuranceStart);
      this.insuranceMinEndDate = { year: startDate.getFullYear(), month: startDate.getMonth() + 1, day: startDate.getDate() };
    }
  }

  onBankGuaranteeDateChange(): void {
    if (this.model.bankGuarantee.guaranteeStart !== undefined &&
      this.model.bankGuarantee.guaranteeEnd !== null) {
      const startDate = new Date(this.model.bankGuarantee.guaranteeStart);
      this.bankGuaranteeMinEndDate = { year: startDate.getFullYear(), month: startDate.getMonth() + 1, day: startDate.getDate() };
    }
  }
}