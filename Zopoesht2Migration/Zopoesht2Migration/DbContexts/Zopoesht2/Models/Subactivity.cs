using System;
using System.Collections.Generic;

namespace Zopoesht2Migration.DbContexts.Zopoesht2.Models;

public partial class Subactivity
{
    public int Id { get; set; }

    public int Activityid { get; set; }

    public string? Name { get; set; }

    public string? Namealt { get; set; }

    public string? Alias { get; set; }

    public int? Vieworder { get; set; }

    public bool Isactive { get; set; }

    public int Version { get; set; }

    public string? Code { get; set; }

    public virtual Activity Activity { get; set; } = null!;

    public virtual ICollection<Activityoffering> Activityofferings { get; set; } = new List<Activityoffering>();
}
