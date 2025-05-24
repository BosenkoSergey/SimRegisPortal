using System.ComponentModel;

namespace SimRegisPortal.Core.Enums;

public enum UserStatus
{
    [Description("Активний")]
    Active = 0,

    [Description("Видалений")]
    Deleted = 1,

    [Description("Заблокований")]
    Blocked = 2
}