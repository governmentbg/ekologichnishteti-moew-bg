using System.ComponentModel;

namespace Zopoesht.Data.Applications.ApplicationOne.Enums
{
    public enum ApplicationOneType
    {
        [Description("Потенциална щета")]
        Threat = 1,

        [Description("Причинена щета")]
        Damage = 2,

        [Description("Докладвана щета")]
        ReportedDamage = 3
    }
}
