import { CommitState } from '../../modules/application/common/enums/commit-state.enum';
import { ApplicantType } from '../../modules/application/application-one/enums/applicant-type.enum';
import { ApplicationOneType } from '../../modules/application/application-one/enums/application-one-type.enum';
import { UserStatus } from '../../modules/user/enums/user-status.enum';
import { OperatorType } from '../../modules/application/application-one/enums/operator-type.enum';
import { ApplicationHistoryType } from '../../modules/application/common/enums/application-history-type.enum';
import { CaseStatus } from '../../modules/application/application-two/enums/case-status.enum';
import { PaymentSource } from '../../modules/application/application-two/enums/payment-source.enum';
import { ApplicationOneDamage } from '../../modules/application/application-one/enums/application-one-damage.enum';
import { ProsecutorType } from '../../modules/nomenclature/enums/prosecutor-type';
import { ProceedingType } from '../../modules/application/application-two/enums/proceeding-type.enum';
import { ReportType } from '../../modules/report/enums/report-type.enum';
import { MortgageType } from '../../modules/application/application-two/enums/mortgage-type.enum';

export const ApplicationOneTypeLocalization = {
  [ApplicationOneType.damage]: 'Причинена екологична щета',
  [ApplicationOneType.threat]: 'Непосредствена заплаха за екологична щета',
  [ApplicationOneType.reportedDamage]: 'Случай с искане за предприемане на действия'
};

export const SummaryTypeBasedOnApplicationOneLocalizaiton = {
  [ApplicationOneType.damage]: 'Случай на причинена екологична щета',
  [ApplicationOneType.threat]: 'Случай на непосредствена заплаха за екологична щета',
  [ApplicationOneType.reportedDamage]: 'Случай с искане за предприемане на действия'
}

export const SummaryTypeBasedOnApplicationOneEnLocalizaiton = {
  [ApplicationOneType.damage]: 'Case of environmental damage',
  [ApplicationOneType.threat]: 'Case of imminent threat of environmental damage',
  [ApplicationOneType.reportedDamage]: 'Action request case'
}

export const UserStatusLocalization = {
  [UserStatus.active]: 'Активен',
  [UserStatus.inactive]: 'Неактивиран',
  [UserStatus.deactivated]: 'Деактивиран'
};

export const ApplicantTypeLocalization = {
  [ApplicantType.operator]: 'Оператор',
  [ApplicantType.authority]: 'Компетентен орган',
  [ApplicantType.individual]: 'Физическо лице',
  [ApplicantType.legalEntity]: 'Юридическо лице',
  [ApplicantType.nonGovernmentalOrganization]: 'НПО'
}

export const CommitStateLocalization = {
  [CommitState.pending]: 'В процес на обработка',
  [CommitState.entered]: 'В очакване на информация по чл. 19а',
  [CommitState.completed]: 'Приключен',
  [CommitState.deleted]: 'Изтрит',
  [CommitState.archived]: 'Архивиран',
  [CommitState.modification]: 'В процес на редакция'
}

export const OperatorTypeLocalization = {
  [OperatorType.person]: 'Физическо лице',
  [OperatorType.legalEntity]: "Юридическо лице"
}

export const ApplicationHistoryTypeLocalization = {
  [ApplicationHistoryType.create]: 'Създаване',
  [ApplicationHistoryType.modification]: 'Започване на редкация',
  [ApplicationHistoryType.complete]: 'Приключване',
  [ApplicationHistoryType.delete]: 'Изтриване',
  [ApplicationHistoryType.restore]: 'Възстановяване',
  [ApplicationHistoryType.cancelModification]: 'Отказване на редакция',
  [ApplicationHistoryType.finishModification]: 'Приключване на редакция'
}

export const CaseStatusLocalization = {
  [CaseStatus.completed]: 'Приключен случай',
  [CaseStatus.active]: 'Текущ случай (в ход на изпълнение)'
}

export const PaymentSourceLocalization = {
  [PaymentSource.authority]: 'По реда на ЗОП с възложител областният управител',
  [PaymentSource.operator]: 'Оператор',
  [PaymentSource.other]: 'Друго'
}

export const ApplicationTwoCommitStateLocalization = {
  [CommitState.pending]: 'В очакване на информация по чл. 19а',
  [CommitState.completed]: 'Приключен',
  [CommitState.archived]: 'Архивиран',
  [CommitState.modification]: 'В процес на редакция'
}

export const ApplicationOneDamageLocalization = {
  [ApplicationOneDamage.waterDamage]: 'Върху води',
  [ApplicationOneDamage.soilDamage]: 'Върху почви',
  [ApplicationOneDamage.speciesDamage]: 'Върху защитени видове и местообитания'
}

export const ProsecutorTypeLozalization = {
  [ProsecutorType.nationalInvestigationService]: 'Национална Следствена Служба',
  [ProsecutorType.appealing]: 'Апелативна прокуратура',
  [ProsecutorType.district]: 'Окръжна прокуратура',
  [ProsecutorType.regional]: 'Районна прокуратура'
}

export const ProceedingTypeLocalization = {
  [ProceedingType.preTrial]: 'Досъдебно производство',
  [ProceedingType.legal]: 'Съдебно производство',
  [ProceedingType.both]: 'Досъдебно производство и съдебно производство'
}

export const ReportTypeLocalization = {
  [ReportType.territorialRange]: 'Териториален обхват',
  [ReportType.applicantType]: 'Заявител'
}

export const MortgageTypeLocalization = {
  [MortgageType.realEstate]: 'Върху неджим/и имот/и',
  [MortgageType.itemizationRightsOfRealEstate]: 'Върху вещно/и права върху недвижими имоти'
}