using Zopoesht.Data.Common.Interfaces;

namespace Zopoesht.Data.Applications.ApplicationTwo.FinancialAssurance
{
    public class Stake : IEntity
    {
        public int Id { get; set; }
        
        public string StakeNumber { get; set; }
        public DateTime? StakeDate { get; set; }
        public string StakeDescription { get; set; }

        public decimal Value { get; set; }

        public string AdditionalInformation { get; set; }
    }
}