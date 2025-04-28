using SimRegisPortal.Core.Entities.Base;

namespace SimRegisPortal.Core.Entities;

public sealed class Company : BaseEntity<Guid>
{
    public string Name { get; set; } = null!;
    public string? TaxNumber { get; set; }
    public string? RegistrationNumber { get; set; }
    public string? Address { get; set; }
    public string? Phone { get; set; }
    public string? Email { get; set; }
    public string? ContactPerson { get; set; }
    public string? Notes { get; set; }

    public ICollection<Project> Projects { get; set; } = [];
}