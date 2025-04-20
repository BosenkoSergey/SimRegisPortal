using SimRegisPortal.Core.Entities.Base;

namespace SimRegisPortal.Core.Entities;

public sealed class Employee : BaseEntity<Guid>
{
    public Guid? UserId { get; set; }
    public string FirstName { get; set; } = null!;
    public string MiddleName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string? Position { get; set; }
    public DateTime HireDate { get; set; }
    public DateTime? DismissalDate { get; set; }
    public decimal HourlyRate { get; set; }

    public User? User { get; set; }
    public ICollection<EmployeeActivity> Activities { get; set; } = [];
}

