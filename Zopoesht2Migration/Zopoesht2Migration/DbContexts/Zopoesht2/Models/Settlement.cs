using System;
using System.Collections.Generic;

namespace Zopoesht2Migration.DbContexts.Zopoesht2.Models;

public partial class Settlement
{
    public int Id { get; set; }

    public int Municipalityid { get; set; }

    public int Districtid { get; set; }

    public string? Municipalitycode { get; set; }

    public string? Districtcode { get; set; }

    public string? Municipalitycode2 { get; set; }

    public string? Districtcode2 { get; set; }

    public string? Typename { get; set; }

    public string? Settlementname { get; set; }

    public string? Typecode { get; set; }

    public string? Mayoraltycode { get; set; }

    public string? Category { get; set; }

    public string? Altitude { get; set; }

    public bool Isdistrict { get; set; }

    public string? Name { get; set; }

    public string? Namealt { get; set; }

    public string? Alias { get; set; }

    public int? Vieworder { get; set; }

    public bool Isactive { get; set; }

    public int Version { get; set; }

    public string? Code { get; set; }

    public virtual District District { get; set; } = null!;

    public virtual Municipality Municipality { get; set; } = null!;

    public virtual ICollection<Operatorcorrespondence> Operatorcorrespondences { get; set; } = new List<Operatorcorrespondence>();

    public virtual ICollection<Operator> Operators { get; set; } = new List<Operator>();

    public virtual ICollection<Terrain> Terrains { get; set; } = new List<Terrain>();
}
