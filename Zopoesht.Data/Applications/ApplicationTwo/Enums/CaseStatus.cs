using System.ComponentModel;

namespace Zopoesht.Data.Applications.ApplicationTwo.Enums
{
    public enum CaseStatus
    {
        [Description("Приключен случай")]
        Completed = 1,

        [Description("Текущ случай (в ход на изпълнение)")]
        Active = 2
    }
}