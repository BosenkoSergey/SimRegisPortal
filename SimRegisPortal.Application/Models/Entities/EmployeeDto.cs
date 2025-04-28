using SimRegisPortal.Core.Enums;

namespace SimRegisPortal.Application.Models.Entities;

public sealed class EmployeeDto : BaseEntityDto<Guid>
{
    public Guid? UserId { get; set; }
    public string FirstName { get; set; } = null!;
    public string MiddleName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string? Position { get; set; }
    public DateTime HireDate { get; set; }
    public DateTime? DismissalDate { get; set; }
    public decimal HourlyRate { get; set; }
    public int HourlyRateCurrencyId { get; set; }
    public SalaryScheme SalaryScheme { get; set; }
}
