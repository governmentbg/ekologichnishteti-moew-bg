using Zopoesht.Data.Common.Interfaces;

namespace Zopoesht.Data.Applications.ApplicationOne
{
    public class ApplicationOneThreat : IEntity
    {
        public int Id { get; set; }

        // Предприетите до момента от оператора превантивни мерки
        public string PreventiveMeasuresTaken { get; set; }

        // Данни от протоколи от извършени анализи и измервания, доказващи нарушаване на приложимите емисионни норми и ограничения
        public string AnalyticProtocols { get; set; }

        // Предложения за други превантивни мерки
        public string MeasuresAdvice { get; set; }

        // Финансов разчет на разходите за изпълнението им
        public string FinancialStatement { get; set; }
    }
}
