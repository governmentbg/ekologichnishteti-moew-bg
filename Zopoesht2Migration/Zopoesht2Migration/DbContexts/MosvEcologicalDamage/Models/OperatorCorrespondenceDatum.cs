using System;
using System.Collections.Generic;

namespace Zopoesht2Migration.DbContexts.MosvEcologicalDamage.Models;

public partial class OperatorCorrespondenceDatum
{
    public int Id { get; set; }

    public int? SettlementId { get; set; }

    public string? Neighborhood { get; set; }

    public string? Street { get; set; }

    public string? StreetNumber { get; set; }

    public string? ApartmentBuildingNumber { get; set; }

    public string? ApartmentBuildingEntrance { get; set; }

    public string? ApartmentBuildingFloor { get; set; }

    public string? ApartmentBuildingFlat { get; set; }

    public string? Phone { get; set; }

    public string? Fax { get; set; }

    public string? Email { get; set; }

    public string? ContactPerson { get; set; }

    public int Version { get; set; }

    public string? PostalCode { get; set; }

    public virtual ICollection<OperatorCorrespondenceDataPart> OperatorCorrespondenceDataParts { get; set; } = new List<OperatorCorrespondenceDataPart>();
}
