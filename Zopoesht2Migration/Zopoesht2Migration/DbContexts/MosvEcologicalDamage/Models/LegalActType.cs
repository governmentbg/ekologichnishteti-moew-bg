using System;
using System.Collections.Generic;

namespace Zopoesht2Migration.DbContexts.MosvEcologicalDamage.Models;

public partial class LegalActType
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public int? ViewOrder { get; set; }

    public bool IsActive { get; set; }

    public int Version { get; set; }

    public virtual ICollection<LegalAct> LegalActs { get; set; } = new List<LegalAct>();
}
