using Zopoesht.Data.Applications.ApplicationOne;
using Zopoesht.Data.Applications.ApplicationTwo.Enums;
using Zopoesht.Data.Applications.ApplicationTwo.FinancialAssurance;
using Zopoesht.Data.Applications.Common.Enums;
using Zopoesht.Data.Applications.Common.Models;
using Zopoesht.Data.Nomenclatures.Operators;
using Zopoesht.Data.Nomenclatures.StateAdministration.Enums;

namespace Zopoesht.Data.Applications.ApplicationTwo
{
    public class ApplicationTwo : ApplicationCommit
    {
        public int ApplicationOneId { get; set; }

        // Вид на непосредствената заплаха за екологични щети 
        // Вид на причинените екологични щети
        public bool HasWaterDamage { get; set; }
        public bool HasSoilDamage { get; set; }
        public bool HasSpeciesDamage { get; set; }

        // Дата на възникване на непосредствената заплаха за екологични щети 
        // Дата  на причинените екологични щети 
        public DateTime? OccurrenceDate { get; set; }
        public string OccurrenceDateDescription { get; set; }
        public string OccurrenceDateDescriptionAlt { get; set; }

        // Датата, на която това е установено 
        public DateTime? ConfirmedDate { get; set; }

        // Дата, на която е започнала процедура за предотвратяване или отстраняване на непосредствена заплаха за екологични щети или на причинените екологични щети
        public DateTime? ProcedureInitiatedDate { get; set; }
        public string ProcedureInitiatedDateDescription { get; set; }
        public string ProcedureInitiatedDateDescriptionAlt { get; set; }

        // Заявител на процедурата по предотвратяване или отстраняване на непосредствена заплаха за екологични щети или на причинени екологични щети - отговорен оператор, компетентен орган или представител на обществеността
        public int ApplicantId { get; set; }
        public Applicant Applicant { get; set; }

        public int? OperatorId { get; set; }
        public Operator Operator { get; set; }

        public ActivityOfferingType? ActivityOfferingType { get; set; }
        public string NotListedDescription { get; set; }
        public string DiffuseNatureDescription { get; set; }

        // Дейности съгласно приложение № 1, в резултат на която е възникнала непосредствената заплаха за екологични щети или са причинени екологични щети
        public List<ApplicationTwoActivityOffering> ApplicationTwoActivityOfferings { get; set; } = new List<ApplicationTwoActivityOffering>();

        public List<ApplicationTwoAuthority> ApplicationTwoAuthorities { get; set; } = new List<ApplicationTwoAuthority>();


        // Класификационенни кодове по Класификацията на икономическите дейности на Националния статистически институт на дейността, в резултат на които е настъпила екологичната щета
        public List<ApplicationTwoCode> Codes { get; set; } = new List<ApplicationTwoCode>();

        // Oбразувани досъдебни производства или съдебни дела във връзка с непосредствена заплаха за екологични щети 
        // Образувани досъдебни производства или съдебни дела във връзка с причинени екологични щети
        public bool ProceedingInfoAbsence { get; set; }
        public ProceedingType? ProceedingType { get; set; }
        public List<ApplicationTwoAdministrativeCourt> ApplicationTwoAdministrativeCourts { get; set; }
        public List<ApplicationTwoProsecutor> ApplicationTwoProsecutors { get; set; }
        public List<ApplicationTwoMinistryOfInterior> ApplicationTwoMinistryOfInteriors { get; set; }
        public string ProceedingInfo { get; set; }


        // Резултат от досъдебните производства или съдебните дела по т. 11 или 12
        public string LegalProcedureResult { get; set; }

        // Резултат от процедурата по предотвратяване на непосредствената заплаха за екологични щети или на екологичните щети
        public bool HasPreventionProcedureResult { get; set; }
        public string PreventionProcedureResult { get; set; }

        // Резултат от процедурата по отстраняване на непосредствената заплаха за екологични щети или екологичните щети
        public bool HasRemovalProcedureResult { get; set; }
        public string RemovalProcedureResult { get; set; }


        // Дата на приключване на процедурата по предотвратяване или отстраняване на непосредствената заплаха за екологични щети или на причинените екологични щети
        public CaseStatus CaseStatus { get; set; }
        public DateTime? PreventionOrRemovalProcedureFinishDate { get; set; }
        public string PreventionOrRemovalProcedureFinishInformation { get; set; }

        // Предприети действия по ЗОПОЕЩ
        public string ActionsTaken { get; set; }
        public string ActionsTakenAlt { get; set; }

        // Разходи за съответните превантивни или оздравителни мерки
        // Заплатени пряко от отговорните страни
        public decimal? PaidByResponsibleParties { get; set; }
        // Възстановени впоследствие от отговорните страни
        public decimal? RecoveredSubsequentlyByResponsibleParties { get; set; }
        // Невъзстановени от отговорните страни, като се посочват причините за невъзстановяването
        public bool HasUnreimbursedExpense { get; set; }
        public decimal? UnreimbursedExpenseValue { get; set; }
        public string UnreimbursedExpense { get; set; }
        // Свободен текст
        public string AdditionalExpenseText { get; set; }

        // Източник за заплащане на разходите
        public PaymentSource PaymentSource { get; set; }
        public string OtherPaymentSource { get; set; }

        // Прилагане на финансово осигуряване по чл. 43, 43а и 43б
        public int? InsurancePolicyId { get; set; }
        public InsurancePolicy InsurancePolicy { get; set; }

        public int? BankGuaranteeId { get; set; }
        public BankGuarantee BankGuarantee { get; set; }

        public int? MortgageId { get; set; }
        public Mortgage Mortgage { get; set; }

        public int? StakeId {  get; set; }
        public Stake Stake { get; set; }

        public bool HasNoCollateral { get; set; }
        public string NoCollateralAdditionalInformation { get; set; }

        // Друга информация
        public string AdditionalInformation { get; set; }

        public List<ApplicationTwoFile> ApplicationTwoFiles { get; set; } = new List<ApplicationTwoFile>();
    }
}
