namespace Zopoesht.Application.Reports.Dtos
{
    public abstract class BaseDamageTypeDto
    {
        public int WaterDamageCount { get; set; }

        public int SoilDamageCount { get; set; }

        public int SpeciesDamageCount { get; set; }

        public int TotalCount { get; set; }
    }
}
