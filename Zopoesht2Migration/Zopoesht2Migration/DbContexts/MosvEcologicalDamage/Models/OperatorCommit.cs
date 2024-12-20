using System;
using System.Collections.Generic;
using Zopoesht2Migration.DbContexts.MosvEcologicalDamage.Enums;

namespace Zopoesht2Migration.DbContexts.MosvEcologicalDamage.Models;

public partial class OperatorCommit
{
    public int Id { get; set; }

    public int LotId { get; set; }

    public CommitState State { get; set; }

    public int? Number { get; set; }

    public int? CommitUserId { get; set; }

    public DateTime? CommitDate { get; set; }

    public int? DocId { get; set; }

    public int? CommitModificationReasonId { get; set; }

    public string? CommitNote { get; set; }

    public int? CommitFileId { get; set; }

    public int Version { get; set; }

    public bool SentEmail { get; set; }

    public string? ApplicationCode { get; set; }

    public int? DeclarationFileId { get; set; }

    public bool ConfirmedDeclaration { get; set; }

    public virtual ICollection<ActivityPart> ActivityParts { get; set; } = new List<ActivityPart>();

    public virtual ICollection<LegalActPart> LegalActParts { get; set; } = new List<LegalActPart>();

    public virtual OperatorBasicPart? OperatorBasicPart { get; set; }

    public virtual OperatorCorrespondenceDataPart? OperatorCorrespondenceDataPart { get; set; }

    public virtual ICollection<TerrainPart> TerrainParts { get; set; } = new List<TerrainPart>();
}
