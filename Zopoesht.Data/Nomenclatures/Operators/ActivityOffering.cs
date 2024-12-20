using Zopoesht.Data.Attributes;
using Zopoesht.Data.Common.Interfaces;

namespace Zopoesht.Data.Nomenclatures.Operators
{
    public class ActivityOffering : IEntity
    {
        public int Id { get; set; }

        // Дейност
        public int? ActivityId { get; set; }
        [Skip]
        public Activity Activity { get; set; }

        // Поддейност
        public int? SubActivityId { get; set; }
        [Skip]
        public SubActivity SubActivity { get; set; }

        public int? OperatorId { get; set; }
        [Skip]
        public Operator Operator { get; set; }

        // Площадка
        public int? TerrainId { get; set; }
        [Skip]
        public Terrain Terrain { get; set; }

        // Регионална инспекция по околната среда и водите, на чиято територия се извършва дейността
        public int? AuthorityRiosvId { get; set; }
        [Skip]
        public Authority AuthorityRiosv { get; set; }

        // Басейнова дирекция за управление на водите, в чийто район се извършва дейността
        public int? AuthorityBasinId { get; set; }
        [Skip]
        public Authority AuthorityBasin { get; set; }
    }
}
