using SimRegisPortal.Application.Services.Interfaces;
using SimRegisPortal.Core.Enums;

namespace SimRegisPortal.Application.Factories;

public interface ISalaryCalculatorFactory
{
    ISalaryCalculator GetCalculator(SalaryScheme salaryScheme);
}
