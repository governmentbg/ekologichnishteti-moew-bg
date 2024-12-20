using System;
using System.Collections.Generic;

namespace Zopoesht2Migration.DbContexts.Zopoesht2.Models;

public partial class District
{
    public int Id { get; set; }

    public string? Code2 { get; set; }

    public string? Secondlevelregioncode { get; set; }

    public string? Mainsettlementcode { get; set; }

    public string? Name { get; set; }

    public string? Namealt { get; set; }

    public string? Alias { get; set; }

    public int? Vieworder { get; set; }

    public bool Isactive { get; set; }

    public int Version { get; set; }

    public string? Code { get; set; }

    public virtual ICollection<Municipality> Municipalities { get; set; } = new List<Municipality>();

    public virtual ICollection<Settlement> Settlements { get; set; } = new List<Settlement>();

    public virtual ICollection<Terrain> Terrains { get; set; } = new List<Terrain>();
}
