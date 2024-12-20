using System;
using System.Collections.Generic;

namespace Zopoesht2Migration.DbContexts.MosvEcologicalDamage.Models;

public partial class LegalActPart
{
    public int Id { get; set; }

    public int CommitId { get; set; }

    public int EntityId { get; set; }

    public int State { get; set; }

    public int? InitialPartId { get; set; }

    public int Version { get; set; }

    public virtual OperatorCommit Commit { get; set; } = null!;

    public virtual LegalAct Entity { get; set; } = null!;

    public virtual LegalActPart? InitialPart { get; set; }

    public virtual ICollection<LegalActPart> InverseInitialPart { get; set; } = new List<LegalActPart>();
}
