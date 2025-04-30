namespace SimRegisPortal.Core.Enums;

public enum UserPermissionType
{
    UsersRead = 100,
    UsersWrite = 110,

    EmployeesRead = 200,
    EmployeesWrite = 210,

    CompaniesRead = 300,
    CompaniesWrite = 310,

    ProjectsRead = 400,
    ProjectsWrite = 410,

    ContractsRead = 500,
    ContractsWrite = 510,

    TimeReportsReadAll = 600,
    TimeReportsWriteAll = 610,

    ActivitiesReadAll = 700,
    ActivitiesWriteAll = 710,

    PaymentRequestsRead = 800,
    PaymentRequestsWrite = 810,

    ExchangeRatesRead = 900,
    ExchangeRatesWrite = 910,

    TaxSettingsRead = 1000,
    TaxSettingsWrite = 1010,
}
