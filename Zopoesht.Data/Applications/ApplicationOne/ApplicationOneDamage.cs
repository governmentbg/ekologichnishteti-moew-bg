using Zopoesht.Data.Common.Interfaces;

namespace Zopoesht.Data.Applications.ApplicationOne
{
    public class ApplicationOneDamage : IEntity
    {
        public int Id { get; set; }

        // Предполагаемите последствия от екологичните щети
        public string OccurenceConsequences { get; set; }

        // Приложените до момента мерки
        public string MeasuresTaken { get; set; }
    }
}
