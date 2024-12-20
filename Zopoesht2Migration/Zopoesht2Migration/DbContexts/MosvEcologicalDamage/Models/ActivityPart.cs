using System;
using System.Collections.Generic;

namespace Zopoesht2Migration.DbContexts.MosvEcologicalDamage.Models;

public partial class ActivityPart
{
    public int Id { get; set; }

    public int CommitId { get; set; }

    public int EntityId { get; set; }

    public int State { get; set; }

    public int? InitialPartId { get; set; }

    public int Version { get; set; }

    public virtual OperatorCommit Commit { get; set; } = null!;

    public virtual Activity Entity { get; set; } = null!;

    public virtual ActivityPart? InitialPart { get; set; }

    public virtual ICollection<ActivityPart> InverseInitialPart { get; set; } = new List<ActivityPart>();
}
