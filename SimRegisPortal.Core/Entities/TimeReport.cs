﻿using SimRegisPortal.Core.Enums;

namespace SimRegisPortal.Core.Entities;

public sealed class TimeReport : BaseEntity<Guid>
{
    public Guid EmployeeId { get; set; }
    public int Year { get; set; }
    public Month Month { get; set; }
    public TimeReportStatus Status { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }

    public Employee Employee { get; set; } = default!;
    public ICollection<EmployeeActivity> Activities { get; set; } = [];
    public ICollection<PaymentRequest> PaymentRequests { get; set; } = [];

    public DateTime GetConversionDate()
    {
        var reportPeriodEnd = new DateTime(Year, (int)Month, DateTime.DaysInMonth(Year, (int)Month));
        return reportPeriodEnd < DateTime.UtcNow.Date ? reportPeriodEnd : DateTime.UtcNow.Date;
    }
}