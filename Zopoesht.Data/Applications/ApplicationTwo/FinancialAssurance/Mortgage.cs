using Zopoesht.Data.Applications.ApplicationTwo.Enums;
using Zopoesht.Data.Common.Interfaces;

namespace Zopoesht.Data.Applications.ApplicationTwo.FinancialAssurance
{
    public class Mortgage : IEntity
    {
        public int Id { get; set; }

        public MortgageType MortgageType { get; set; }
        public string MortgageNumber { get; set; }
        public DateTime? MortgageDate { get; set; }
        public string MortgageDescription { get; set; }

        public decimal Value { get; set; }

        public string AdditionalInformation {  get; set; }
    }
}
