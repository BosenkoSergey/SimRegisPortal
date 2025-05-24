using System.ComponentModel;

namespace SimRegisPortal.Core.Enums;

public enum PaymentRequestType
{
    [Description("Єдиний соціальний внесок")]
    SocialTax = 1,

    [Description("Єдиний податок")]
    PIT = 2,

    [Description("Військовий збір")]
    MilitaryTax = 3,

    [Description("Чиста заробітна плата")]
    NetSalary = 4
}