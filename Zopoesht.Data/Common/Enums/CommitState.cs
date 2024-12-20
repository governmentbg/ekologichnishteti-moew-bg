using System.ComponentModel;

namespace Zopoesht.Data.Common.Enums
{
    public enum CommitState
    {
        [Description("В процес на обработка")]
        Pending = 1,

        [Description("Вписано")]
        Entered = 2,

        [Description("Приключено")]
        Completed = 3,

        [Description("Изтрито")]
        Deleted = 4,

        [Description("Архивирано")]
        Archived = 5,

        [Description("В процес на редакция")]
        Modification = 6
    }
}