using Microsoft.Extensions.DependencyInjection;
using SimRegisPortal.Application.Services.Interfaces;
using SimRegisPortal.Application.Services.SalaryCalculators;
using SimRegisPortal.Core.Enums;

namespace SimRegisPortal.Application.Factories;

public sealed class SalaryCalculatorFactory(IServiceProvider ServiceProvider) : ISalaryCalculatorFactory
{
    public ISalaryCalculator GetCalculator(SalaryScheme salaryScheme)
    {
        return salaryScheme switch
        {
            SalaryScheme.FOP2 => ServiceProvider.GetRequiredService<Fop2SalaryCalculator>(),
            SalaryScheme.FOP3 => ServiceProvider.GetRequiredService<Fop3SalaryCalculator>(),
            SalaryScheme.GIG => ServiceProvider.GetRequiredService<GigSalaryCalculator>(),
            _ => throw new NotSupportedException()
        };
    }
}
