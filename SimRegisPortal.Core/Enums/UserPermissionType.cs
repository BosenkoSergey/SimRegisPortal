namespace SimRegisPortal.Core.Enums;

using System.ComponentModel;

public enum UserPermissionType
{
    [Description("users-read")]
    UsersRead = 100,

    [Description("users-write")]
    UsersWrite = 110,

    [Description("employees-read")]
    EmployeesRead = 200,

    [Description("employees-write")]
    EmployeesWrite = 210,

    [Description("companies-read")]
    CompaniesRead = 300,

    [Description("companies-write")]
    CompaniesWrite = 310,

    [Description("projects-read")]
    ProjectsRead = 400,

    [Description("projects-write")]
    ProjectsWrite = 410,

    [Description("contracts-read")]
    ContractsRead = 500,

    [Description("contracts-write")]
    ContractsWrite = 510,

    [Description("all-timere-ports-read")]
    TimeReportsReadAll = 600,

    [Description("all-timere-ports-write")]
    TimeReportsWriteAll = 610,

    [Description("all-activities-read")]
    ActivitiesReadAll = 700,

    [Description("all-activities-write")]
    ActivitiesWriteAll = 710,

    [Description("payment-requests-read")]
    PaymentRequestsRead = 800,

    [Description("payment-requests-write")]
    PaymentRequestsWrite = 810,

    [Description("exchange-rates-read")]
    ExchangeRatesRead = 900,

    [Description("exchange-rates-write")]
    ExchangeRatesWrite = 910,

    [Description("tax-settings-read")]
    TaxSettingsRead = 1000,

    [Description("tax-settings-write")]
    TaxSettingsWrite = 1010,

    [Description("system-logs-read")]
    SystemLogsRead = 1100,
}
