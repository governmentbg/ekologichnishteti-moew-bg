using System;
using System.Collections.Generic;

namespace Zopoesht2Migration.DbContexts.Zopoesht2.Models;

public partial class Terrain
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public int? Districtid { get; set; }

    public int? Municipalityid { get; set; }

    public int? Settlementid { get; set; }

    public string? Address { get; set; }

    public int Operatorid { get; set; }

    public string? Alias { get; set; }

    public bool Isactive { get; set; }

    public string? Namealt { get; set; }

    public int Version { get; set; }

    public int? Vieworder { get; set; }

    public int? Migrationid { get; set; }

    public virtual ICollection<Activityoffering> Activityofferings { get; set; } = new List<Activityoffering>();

    public virtual District? District { get; set; }

    public virtual Municipality? Municipality { get; set; }

    public virtual Operator Operator { get; set; } = null!;

    public virtual Settlement? Settlement { get; set; }
}
