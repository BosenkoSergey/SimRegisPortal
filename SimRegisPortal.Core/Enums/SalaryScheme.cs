using System.ComponentModel;

namespace SimRegisPortal.Core.Enums;

public enum SalaryScheme
{
    [Description("ФОП 2 групи")]
    FOP2 = 1,

    [Description("ФОП 3 групи")]
    FOP3 = 2,

    [Description("Гіг-контракт")]
    GIG = 3
}