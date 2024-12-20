using System;
using System.Collections.Generic;

namespace Zopoesht2Migration.DbContexts.MosvEcologicalDamage.Models;

public partial class LegalAct
{
    public int Id { get; set; }

    public int? TypeId { get; set; }

    public string? Number { get; set; }

    public DateTime? IssueDate { get; set; }

    public DateTime? ValidUntilDate { get; set; }

    public string? Description { get; set; }

    public string? Note { get; set; }

    public string? LastModificationNumber { get; set; }

    public int AdministrativeUnitId { get; set; }

    public int Version { get; set; }

    public int? KrLotId { get; set; }

    public int? KrLotRevisionId { get; set; }

    public int? KrOrdinal { get; set; }

    public virtual AdministrativeUnit AdministrativeUnit { get; set; } = null!;

    public virtual ICollection<LegalActPart> LegalActParts { get; set; } = new List<LegalActPart>();

    public virtual LegalActType? Type { get; set; }
}
