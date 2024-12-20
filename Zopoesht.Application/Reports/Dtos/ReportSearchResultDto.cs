namespace Zopoesht.Application.Reports.Dtos
{
    public class ReportSearchResultDto
    {
        public ReportSearchResultDto(string itemName)
        {
            ItemName = itemName;
            Threat = new ThreatDto();
            Damage = new DamageDto();
            ReportedDamage = new ReportedDamageDto();
        }

        public string ItemName { get; set; }

        public int ItemCount { get; set; }

        public ThreatDto Threat { get; set; }

        public DamageDto Damage { get; set; }

        public ReportedDamageDto ReportedDamage { get; set; }
    }
}
