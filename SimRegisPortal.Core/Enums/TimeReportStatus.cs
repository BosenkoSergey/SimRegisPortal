using System.ComponentModel;

namespace SimRegisPortal.Core.Enums;

public enum TimeReportStatus
{
    [Description("Новий")]
    New = 0,

    [Description("Фіналізований")]
    Finalized = 1,

    [Description("Відхилений")]
    Rejected = 2,

    [Description("Схвалений")]
    Approved = 3,

    [Description("Закритий")]
    Closed = 4
}
