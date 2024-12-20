using System.ComponentModel;

namespace Zopoesht.Data.Applications.ApplicationTwo.Enums
{
    public enum PaymentSource
    {
        [Description("По реда на ЗОП с възложител областният управител")]
        Authority = 1,

        [Description("Оператор")]
        Operator = 2,

        [Description("Друго")]
        Other = 3
    }
}
