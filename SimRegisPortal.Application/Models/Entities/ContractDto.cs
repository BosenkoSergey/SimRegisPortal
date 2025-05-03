namespace SimRegisPortal.Application.Models.Entities;

public sealed class ContractDto : BaseEntityDto<Guid>
{
    public Guid ProjectId { get; set; }
    public Guid EmployeeId { get; set; }
    public string Number { get; set; } = null!;
    public string? Notes { get; set; }
    public DateTime? DateFrom { get; set; }
    public DateTime? DateTo { get; set; }
}
