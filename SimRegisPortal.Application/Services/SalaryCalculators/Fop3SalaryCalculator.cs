﻿using SimRegisPortal.Application.Services.Interfaces;
using SimRegisPortal.Core.Entities;
using SimRegisPortal.Persistence.Context;

namespace SimRegisPortal.Application.Services.SalaryCalculators;

public sealed class Fop3SalaryCalculator(AppDbContext dbContext, ICurrencyConverter currencyConverter)
    : BaseUaSalaryCalculator(dbContext, currencyConverter)
{
    protected override decimal GetPitAmount(decimal originalAmount)
    {
        var baseAmount = originalAmount - GetSocialTaxAmount();
        return baseAmount * TaxSetting.Fop3Pit / 100;
    }

    protected override decimal GetMilitaryTaxAmount(decimal originalAmount)
    {
        var baseAmount = originalAmount - GetSocialTaxAmount();
        return baseAmount * TaxSetting.Fop3MilitaryTax / 100;
    }
}
