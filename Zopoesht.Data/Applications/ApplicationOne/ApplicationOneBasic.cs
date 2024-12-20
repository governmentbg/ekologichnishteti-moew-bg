using Zopoesht.Data.Applications.Common.Enums;
using Zopoesht.Data.Attributes;
using Zopoesht.Data.Common.Interfaces;
using Zopoesht.Data.Nomenclatures;
using Zopoesht.Data.Nomenclatures.Operators;

namespace Zopoesht.Data.Applications.ApplicationOne
{
    public class ApplicationOneBasic : IEntity
    {
        public int Id { get; set; }

        public int ApplicationOneId { get; set; }
        public ApplicationOne ApplicationOne { get; set; }

        // Входящ номер на документа 
        public string IncomingDocNumber { get; set; }

        // Наименование на случая
        public string Name { get; set; }
        public string NameAlt { get; set; }

        // Данни за оператора, свързан с екологичната щета

        public int? OperatorId { get; set; }
        [Skip]
        public Operator Operator { get; set; }

        public ActivityOfferingType? ActivityOfferingType { get; set; }
        public string NotListedDescription { get; set; }
        public string DiffuseNatureDescription { get; set; }

        // Дейност извършвана от оператор
        public List<ApplicationOneActivityOffering> ActivityOfferings { get; set; } = new List<ApplicationOneActivityOffering>();

        // Вид на причинените/потенциалните екологични щети
        public bool HasWaterDamage { get; set; }
        public bool HasSoilDamage { get; set; }
        public bool HasSpeciesDamage { get; set; }

        // Териториален обхват на причинените/потенциалните екологични щети
        public List<ApplicationOneTerritorialRange> TerritorialRanges { get; set; } = new List<ApplicationOneTerritorialRange>();

        // Място на причинените/потенциалните екологични щети
        public string OccurenceLocation { get; set; }

        //  Действителни или предполагаеми причини за настъпването на причинени/потенциални екологичните щети
        public string OccurenceReasons { get; set; }

        // Други обстоятелства и факти, подкрепящи информацията и наблюденията във връзка с причинените/потенциалните екологични щети
        public string AdditionalInformation { get; set; }

        public bool HasInternationalElement { get; set; }
        public string InternationalElementDescription { get; set; }
        public int? CulpritCountryId { get; set; }
        public Country CulpritCountry { get; set; }
        public List<ApplicationOneAffectedCountry> AffectedCountries { get; set; } = new List<ApplicationOneAffectedCountry>();
    }
}