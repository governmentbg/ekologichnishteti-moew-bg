using System;
using System.Collections.Generic;

namespace Zopoesht2Migration.DbContexts.MosvEcologicalDamage.Models;

public partial class OperatorCorrespondenceDataPart
{
    public int Id { get; set; }

    public int Version { get; set; }

    public int EntityId { get; set; }

    public int State { get; set; }

    public virtual OperatorCorrespondenceDatum Entity { get; set; } = null!;

    public virtual OperatorCommit IdNavigation { get; set; } = null!;
}
