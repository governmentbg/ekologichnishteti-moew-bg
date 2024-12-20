using System;
using System.Collections.Generic;
using Zopoesht2Migration.DbContexts.Zopoesht2.Enums;

namespace Zopoesht2Migration.DbContexts.Zopoesht2.Models;

public partial class Operator
{
    public int Id { get; set; }

    public string? Firstname { get; set; }

    public string? Middlename { get; set; }

    public string? Lastname { get; set; }

    public OperatorType Type { get; set; }

    public string? Legalentityname { get; set; }

    public string? Legalentityuic { get; set; }

    public int? Settlementid { get; set; }

    public string? Managementaddress { get; set; }

    public string? Postalcode { get; set; }

    public int? Migrationid { get; set; }

    public int Operatorcorrespondenceid { get; set; }

    public string? Name { get; set; }

    public string? Namealt { get; set; }

    public string? Alias { get; set; }

    public int? Vieworder { get; set; }

    public bool Isactive { get; set; }

    public int Version { get; set; }

    public virtual ICollection<Activityoffering> Activityofferings { get; set; } = new List<Activityoffering>();

    public virtual Operatorcorrespondence Operatorcorrespondence { get; set; } = null!;

    public virtual Settlement? Settlement { get; set; }

    public virtual ICollection<Terrain> Terrains { get; set; } = new List<Terrain>();
}
