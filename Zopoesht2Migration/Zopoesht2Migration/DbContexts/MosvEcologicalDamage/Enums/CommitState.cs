using System.ComponentModel;

namespace Zopoesht2Migration.DbContexts.MosvEcologicalDamage.Enums
{
    public enum CommitState
    {
        [Description("първоначална чернова")]
        InitialDraft = 1,

        [Description("в процес на промяна")]
        Modification = 2,

        [Description("актуално състояние")]
        Actual = 3,

        [Description("актуално с инициирана промяна")]
        ActualWithModification = 4,

        [Description("предишно състояние")]
        History = 5,

        [Description("заличено")]
        Deleted = 6,

        [Description("готов за вписване")]
        CommitReady = 7
    }
}
