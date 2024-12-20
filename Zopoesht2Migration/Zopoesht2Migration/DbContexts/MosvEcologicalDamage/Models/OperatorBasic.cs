using System;
using System.Collections.Generic;

namespace Zopoesht2Migration.DbContexts.MosvEcologicalDamage.Models;

public partial class OperatorBasic
{
    public int Id { get; set; }

    public int Type { get; set; }

    public string? FirstName { get; set; }

    public string? MiddleName { get; set; }

    public string? LastName { get; set; }

    public string? LegalEntityName { get; set; }

    public string? LegalEntityUic { get; set; }

    public int? SettlementId { get; set; }

    public string? Neighborhood { get; set; }

    public string? Street { get; set; }

    public string? StreetNumber { get; set; }

    public string? ApartmentBuildingNumber { get; set; }

    public string? ApartmentBuildingEntrance { get; set; }

    public string? ApartmentBuildingFloor { get; set; }

    public string? ApartmentBuildingFlat { get; set; }

    public int? KrLotId { get; set; }

    public int? KrLotRevisionId { get; set; }

    public int? KrOrdinal { get; set; }

    public int Version { get; set; }

    public string? PostalCode { get; set; }

    public string? EraseReason { get; set; }

    public virtual ICollection<OperatorBasicPart> OperatorBasicParts { get; set; } = new List<OperatorBasicPart>();
}
