using SimRegisPortal.Application.Services.Interfaces;
using SimRegisPortal.Core.Entities;

namespace SimRegisPortal.Application.Services.SalaryCalculators;

public sealed class Fop3SalaryCalculator : ISalaryCalculator
{
    public Task<ICollection<PaymentRequest>> CalculateAsync(TimeReport timeReport)
    {
        throw new NotImplementedException();
    }
}
