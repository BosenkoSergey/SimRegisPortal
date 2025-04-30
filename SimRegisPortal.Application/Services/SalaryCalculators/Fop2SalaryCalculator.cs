using SimRegisPortal.Application.Services.Interfaces;
using SimRegisPortal.Core.Entities;
using SimRegisPortal.Persistence.Context;

namespace SimRegisPortal.Application.Services.SalaryCalculators;

public sealed class Fop2SalaryCalculator(AppDbContext dbContext, ICurrencyConverter currencyConverter)
    : BaseUaSalaryCalculator(dbContext, currencyConverter)
{
    protected override decimal GetPitAmount(decimal originalAmount)
    {
        return TaxSetting.MinimumWage * TaxSetting.Fop2Pit / 100;
    }

    protected override decimal GetMilitaryTaxAmount(decimal originalAmount)
    {
        return TaxSetting.MinimumWage * TaxSetting.Fop2MilitaryTax / 100;
    }
}
