using System.ComponentModel;

namespace SimRegisPortal.Core.Enums;

public enum PriorityType
{
    [Description("Низький")]
    Low = 0,

    [Description("Середній")]
    Medium = 1,

    [Description("Високий")]
    High = 2,

    [Description("Найвищій")]
    Critical = 3,
}
