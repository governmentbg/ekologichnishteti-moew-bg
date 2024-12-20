using Zopoesht.Data.Common.Interfaces;

namespace Zopoesht.Data.Applications.ApplicationTwo.FinancialAssurance
{
    public class BankGuarantee : IEntity
    {
        public int Id { get; set; }

        public string BankName { get; set; }
        public string GuaranteeNumber { get; set; }
        public DateTime? GuaranteeDate { get; set; }

        public DateTime? GuaranteeStart {  get; set; }
        public DateTime? GuaranteeEnd {  get; set; }

        public decimal Value { get; set; }

        public string AdditionalInformation { get; set; }
    }
}
