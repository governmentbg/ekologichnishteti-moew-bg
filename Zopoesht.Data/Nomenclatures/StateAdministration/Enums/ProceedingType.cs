using System.ComponentModel;

namespace Zopoesht.Data.Nomenclatures.StateAdministration.Enums
{
    public enum ProceedingType
    {
        [Description("Досъдебно производство")]
        PreTrial = 1,

        [Description("Съдебно производство")]
        Legal = 2,

        [Description("Досъдебно производство и съдебно производство")]
        Both = 3
    }
}