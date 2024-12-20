using Zopoesht.Data.Common.Interfaces;

namespace Zopoesht.Data.Common.Models
{
    public abstract class Nomenclature : IEntity, IConcurrency
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string NameAlt { get; set; }
        public string Alias { get; set; }
        public int? ViewOrder { get; set; }
        public bool IsActive { get; set; } = true;
        public int Version { get; set; }

        public virtual void Update(Nomenclature newModel)
        {
            this.Name = newModel.Name;
            this.ViewOrder = newModel.ViewOrder;
            this.IsActive = newModel.IsActive;
        }
    }
}