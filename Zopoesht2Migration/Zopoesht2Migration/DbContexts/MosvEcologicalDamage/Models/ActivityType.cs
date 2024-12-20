using System;
using System.Collections.Generic;

namespace Zopoesht2Migration.DbContexts.MosvEcologicalDamage.Models;

public partial class ActivityType
{
    public int Id { get; set; }

    public string Code { get; set; } = null!;

    public string? Name { get; set; }

    public int? ParentId { get; set; }

    public int? ViewOrder { get; set; }

    public bool IsActive { get; set; }

    public int Version { get; set; }

    public virtual ICollection<Activity> Activities { get; set; } = new List<Activity>();
}
