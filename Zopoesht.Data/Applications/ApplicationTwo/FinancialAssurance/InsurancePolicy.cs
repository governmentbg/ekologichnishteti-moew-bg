using Zopoesht.Data.Common.Interfaces;

namespace Zopoesht.Data.Applications.ApplicationTwo.FinancialAssurance
{
    public class InsurancePolicy : IEntity
    {
        public int Id { get; set; }

        public string CompanyName { get; set; }
        public string PolicyNumber { get; set; }
        public DateTime? PolicyDate { get; set; }

        public DateTime? InsuranceStart { get; set; }
        public DateTime? InsuranceEnd { get; set; }

        public decimal ResponsibilityLimit { get; set; }

        public string AdditionalInformation { get; set; }
    }
}