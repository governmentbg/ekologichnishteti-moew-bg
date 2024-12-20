using System;
using System.Collections.Generic;

namespace Zopoesht2Migration.DbContexts.MosvEcologicalDamage.Models;

public partial class Terrain
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public int? DistrictId { get; set; }

    public int? MunicipalityId { get; set; }

    public int? SettlementId { get; set; }

    public string? Address { get; set; }

    public string? Contacts { get; set; }

    public string? ZipCode { get; set; }

    public double? Longitude { get; set; }

    public double? Latitude { get; set; }

    public int? KrLotId { get; set; }

    public int? KrLotRevisionId { get; set; }

    public int? KrOrdinal { get; set; }

    public int? AttachedFileId { get; set; }

    public int Version { get; set; }

    public virtual ICollection<TerrainPart> TerrainParts { get; set; } = new List<TerrainPart>();
}
