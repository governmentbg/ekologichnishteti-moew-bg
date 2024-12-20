using System;
using System.Collections.Generic;

namespace Zopoesht2Migration.DbContexts.Zopoesht2.Models;

public partial class Authority
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public string? Namealt { get; set; }

    public string? Alias { get; set; }

    public int? Vieworder { get; set; }

    public bool Isactive { get; set; }

    public int Version { get; set; }

    public int Authoritytype { get; set; }

    public int? Migrationid { get; set; }

    public virtual ICollection<Activityoffering> ActivityofferingAuthoritybasins { get; set; } = new List<Activityoffering>();

    public virtual ICollection<Activityoffering> ActivityofferingAuthorityriosvs { get; set; } = new List<Activityoffering>();
}
