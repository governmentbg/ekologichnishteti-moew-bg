using System;
using System.Collections.Generic;

namespace Zopoesht2Migration.DbContexts.MosvEcologicalDamage.Models;

public partial class Activity
{
    public int Id { get; set; }

    public int ActivityTypeId { get; set; }

    public int? TerrainInitialPartId { get; set; }

    public int? LegalActInitialPartId { get; set; }

    public int? AdministrativeUnitRiosvId { get; set; }

    public virtual AdministrativeUnit AdministrativeUnitRiosv { get; set; } = null!;

    public int? AdministrativeUnitBaseinId { get; set; }

    public virtual AdministrativeUnit AdministrativeUnitBasein { get; set; } = null!;

    public bool HasSpecialRegime { get; set; }

    public int? KrLotId { get; set; }

    public int? KrLotRevisionId { get; set; }

    public int? KrOrdinal { get; set; }

    public int Version { get; set; }

    public string? Other { get; set; }

    public virtual ICollection<ActivityPart> ActivityParts { get; set; } = new List<ActivityPart>();

    public virtual ActivityType ActivityType { get; set; } = null!;
}
