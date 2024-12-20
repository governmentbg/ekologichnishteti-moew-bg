using System;
using System.Collections.Generic;

namespace Zopoesht2Migration.DbContexts.Zopoesht2.Models;

public partial class Activityoffering
{
    public int Id { get; set; }

    public int? Activityid { get; set; }

    public int? Subactivityid { get; set; }

    public int? Operatorid { get; set; }

    public int? Terrainid { get; set; }

    public int? Authorityriosvid { get; set; }

    public int? Authoritybasinid { get; set; }

    public virtual Activity? Activity { get; set; }

    public virtual Authority? Authoritybasin { get; set; }

    public virtual Authority? Authorityriosv { get; set; }

    public virtual Operator? Operator { get; set; }

    public virtual Subactivity? Subactivity { get; set; }

    public virtual Terrain? Terrain { get; set; }
}
