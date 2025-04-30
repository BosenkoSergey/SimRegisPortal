using SimRegisPortal.Application.Services.Interfaces;
using SimRegisPortal.Core.Entities;
using SimRegisPortal.Core.Enums;
using SimRegisPortal.Core.Exceptions;
using SimRegisPortal.Persistence.Context;

namespace SimRegisPortal.Application.Services.SalaryCalculators;

public abstract class BaseUaSalaryCalculator : ISalaryCalculator
{
    protected readonly ICurrencyConverter Converter;
    protected readonly TaxSetting TaxSetting;

    public BaseUaSalaryCalculator(AppDbContext dbContext, ICurrencyConverter currencyConverter)
    {
        Converter = currencyConverter;
        TaxSetting = dbContext.TaxSettings.First();
    }

    public async Task<ICollection<PaymentRequest>> CalculateAsync(TimeReport timeReport)
    {
        var totalHours = timeReport.Activities.Sum(a => a.Hours);
        if (totalHours == 0)
        {
            return [];
        }

        var amount = await Converter.ConvertAsync(
            timeReport.Employee.HourlyRate * totalHours,
            timeReport.Employee.HourlyRateCurrencyId,
            TaxSetting.LocalCurrencyId,
            timeReport.GetConversionDate());

        var socialTaxAmount = Math.Round(GetSocialTaxAmount(), 2);
        var pitAmount = Math.Round(GetPitAmount(amount), 2);
        var militaryTaxAmount = Math.Round(GetMilitaryTaxAmount(amount), 2);

        var netSalaryAmount = amount - socialTaxAmount - pitAmount - militaryTaxAmount;
        if (netSalaryAmount <= 0)
        {
            throw new CommonException("Validation.NetSalary.Negative");
        }

        return
        [
            new() {
                TimeReportId = timeReport.Id,
                Type = PaymentRequestType.SocialTax,
                Amount = socialTaxAmount,
                CurrencyId = TaxSetting.LocalCurrencyId
            },
            new() {
                TimeReportId = timeReport.Id,
                Type = PaymentRequestType.PIT,
                Amount = pitAmount,
                CurrencyId = TaxSetting.LocalCurrencyId
            },
            new() {
                TimeReportId = timeReport.Id,
                Type = PaymentRequestType.MilitaryTax,
                Amount = militaryTaxAmount,
                CurrencyId = TaxSetting.LocalCurrencyId
            },
            new() {
                TimeReportId = timeReport.Id,
                Type = PaymentRequestType.NetSalary,
                Amount = netSalaryAmount,
                CurrencyId = TaxSetting.LocalCurrencyId
            }
        ];
    }

    protected virtual decimal GetSocialTaxAmount()
    {
        return TaxSetting.MinimumWage * TaxSetting.SocialTax / 100;
    }

    protected abstract decimal GetPitAmount(decimal originalAmount);
    protected abstract decimal GetMilitaryTaxAmount(decimal originalAmount);
}
