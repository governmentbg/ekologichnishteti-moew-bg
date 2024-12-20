using System.ComponentModel;

namespace Zopoesht.Data.Applications.ApplicationOne.Enums
{
    public enum ApplicantType
    {
        [Description("Отговорен оператор")]
        Operator = 1,

        [Description("Компетентен орган")]
        Authority = 2,

        [Description("Физическо лице")]
        Individual = 3,

        [Description("Юридическо лице")]
        LegalEntity = 4,

        [Description("Неправителствена организация")]
        NonGovernmentalOrganization = 5
    }
}