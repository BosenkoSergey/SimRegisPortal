﻿using SimRegisPortal.Core.Enums;

namespace SimRegisPortal.Application.Models.Entities.Related;

public sealed class PaymentRequestQueryParams
{
    public Guid? TimeReportId { get; set; }
    public int? Year { get; set; }
    public Month? Month { get; set; }
    public Guid? EmployeeId { get; set; }
    public PaymentRequestType? Type { get; set; }
    public bool? IsPaid { get; set; }

    public PaymentRequestQueryParams()
    {
        Year = DateTime.UtcNow.Year;
        Month = (Month)DateTime.UtcNow.Month;
        IsPaid = false;
    }

    public PaymentRequestQueryParams(Guid timeReportId)
    {
        TimeReportId = timeReportId;
    }
}
