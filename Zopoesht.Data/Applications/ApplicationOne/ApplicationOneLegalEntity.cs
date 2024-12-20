using Zopoesht.Data.Common.Interfaces;

namespace Zopoesht.Data.Applications.ApplicationOne
{
    public class ApplicationOneLegalEntity : IEntity
    {
        public int Id { get; set; }

        // Адрес за кореспонденция
        public string Address { get; set; }

        // Нарушено право на заявителя или достатъчен интерес при вземането на решение за отстраняване на екологични щети
        public string ApplicantViolations { get; set; }

        // Препоръки за предприемането на съответни оздравителни мерки, ако лицето има такива
        public string RecoveryAdvice { get; set; }

        // Видими и/или предполагаеми последствия от екологичните щети
        public string AllegedOccurenceConsequences { get; set; }
    }
}