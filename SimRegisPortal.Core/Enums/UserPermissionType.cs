namespace SimRegisPortal.Core.Enums;

using System.ComponentModel;

public enum UserPermissionType
{
    [Description("Перегляд каталогу користувачів")]
    UsersRead = 100,

    [Description("Редагування користувачів")]
    UsersWrite = 110,

    [Description("Перегляд каталогу працівників")]
    EmployeesRead = 200,

    [Description("Редагування працівників")]
    EmployeesWrite = 210,

    [Description("Перегляд каталогу компаній")]
    CompaniesRead = 300,

    [Description("Редагування компаній")]
    CompaniesWrite = 310,

    [Description("Перегляд каталогу проєктів")]
    ProjectsRead = 400,

    [Description("Редагування проєктів")]
    ProjectsWrite = 410,

    [Description("Перегляд каталогу контрактів")]
    ContractsRead = 500,

    [Description("Редагування контрактів")]
    ContractsWrite = 510,

    [Description("Перегляд каталогу часових звітів всіх співробіників")]
    TimeReportsReadAll = 600,

    [Description("Редагування часових звітів всіх співробіників")]
    TimeReportsWriteAll = 610,

    [Description("Перегляд каталогу активностей всіх співробіників")]
    ActivitiesReadAll = 700,

    [Description("Редагування активностей всіх співробіників")]
    ActivitiesWriteAll = 710,

    [Description("Перегляд каталогу платіжних запитів всіх співробіників")]
    PaymentRequestsReadAll = 800,

    [Description("Редагування платіжних запитів всіх співробіників")]
    PaymentRequestsWrite = 810,

    [Description("Перегляд каталогу курсів валют")]
    ExchangeRatesRead = 900,

    [Description("Редагування курсів валют")]
    ExchangeRatesWrite = 910,

    [Description("Перегляд податкових налаштувань")]
    TaxSettingsRead = 1000,

    [Description("Редагування податкових налаштувань")]
    TaxSettingsWrite = 1010,

    [Description("Перегляд каталогу системних логів")]
    SystemLogsRead = 1100,
}
