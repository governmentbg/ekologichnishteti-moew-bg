using System.ComponentModel;

namespace Zopoesht.Data.Nomenclatures.Operators.Enums
{
    public enum OperatorType
    {
        [Description("Физическо лице")]
        Person = 1,

        [Description("Юридическо лице")]
        LegalEntity = 2,
    }
}
