using System.ComponentModel;

namespace Zopoesht2Migration.DbContexts.Zopoesht2.Enums
{
    public enum OperatorType
    {
        [Description("Физическо лице")]
        Person = 1,

        [Description("Юридическо лице")]
        LegalEntity = 2,
    }
}
