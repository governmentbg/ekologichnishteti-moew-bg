using System;
using System.Collections.Generic;

namespace Zopoesht2Migration.DbContexts.Zopoesht2.Models;

public partial class Activity
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public string? Namealt { get; set; }

    public string? Alias { get; set; }

    public int? Vieworder { get; set; }

    public bool Isactive { get; set; }

    public int Version { get; set; }

    public string? Code { get; set; }

    public virtual ICollection<Activityoffering> Activityofferings { get; set; } = new List<Activityoffering>();

    public virtual ICollection<Subactivity> Subactivities { get; set; } = new List<Subactivity>();
}
