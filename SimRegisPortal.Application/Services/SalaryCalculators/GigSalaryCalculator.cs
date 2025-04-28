using SimRegisPortal.Application.Services.Interfaces;
using SimRegisPortal.Core.Entities;

namespace SimRegisPortal.Application.Services.SalaryCalculators;

public sealed class GigSalaryCalculator : ISalaryCalculator
{
    public Task<ICollection<PaymentRequest>> CalculateAsync(TimeReport timeReport)
    {
        throw new NotImplementedException();
    }
}
