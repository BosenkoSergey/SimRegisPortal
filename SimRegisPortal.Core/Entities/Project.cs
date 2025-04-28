using SimRegisPortal.Core.Entities.Base;

namespace SimRegisPortal.Core.Entities;

public sealed class Project : BaseEntity<Guid>
{
    public Guid CompanyId { get; set; }
    public string Name { get; set; } = null!;
    public string? Description { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }

    public Company Company { get; set; } = null!;
    public ICollection<UserProjectPermission> ProjectPermissions { get; set; } = [];
}
