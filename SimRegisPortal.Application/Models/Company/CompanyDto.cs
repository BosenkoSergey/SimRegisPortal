using SimRegisPortal.Application.Models.Base;

namespace SimRegisPortal.Application.Models.Company;

public sealed class CompanyDto : BaseEntityDto<Guid>
{
    public string Name { get; set; } = null!;
    public string? TaxNumber { get; set; }
    public string? RegistrationNumber { get; set; }
    public string? Address { get; set; }
    public string? Phone { get; set; }
    public string? Email { get; set; }
    public string? ContactPerson { get; set; }
    public string? Notes { get; set; }
}
