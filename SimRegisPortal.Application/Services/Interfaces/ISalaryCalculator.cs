using SimRegisPortal.Core.Entities;

namespace SimRegisPortal.Application.Services.Interfaces;

public interface ISalaryCalculator
{
    Task<ICollection<PaymentRequest>> CalculateAsync(TimeReport timeReport);
}
