using SimRegisPortal.Application.Services.SalaryCalculators;
using SimRegisPortal.Application.Services;
using SimRegisPortal.Core.Entities;
using SimRegisPortal.Core.Enums;
using SimRegisPortal.Persistence.Context;
using SimRegisPortal.Tests.Context;

namespace SimRegisPortal.Tests.Features;

public class Fop3SalaryCalculatorTests
{
    private readonly AppDbContext _context;
    private readonly Fop3SalaryCalculator _calculator;

    public Fop3SalaryCalculatorTests()
    {
        _context = InMemoryDbContextFactory.CreateContext();
        var converter = new CurrencyConverter(_context);
        _calculator = new Fop3SalaryCalculator(_context, converter);
    }

    [Fact]
    public async Task Should_Calculate_SocialTax_Correctly()
    {
        var report = CreateTimeReport(100);
        var results = await _calculator.CalculateAsync(report);

        var socialTax = results.First(x => x.Type == PaymentRequestType.SocialTax);

        Assert.Equal(1760.00m, socialTax.Amount);
    }

    [Fact]
    public async Task Should_Calculate_PIT_Correctly()
    {
        var report = CreateTimeReport(100);
        var results = await _calculator.CalculateAsync(report);

        var pit = results.First(x => x.Type == PaymentRequestType.PIT);

        var gross = 100 * 10 * 43;
        var baseAmount = gross - 8000 * 0.22m;
        var expectedPit = Math.Round(baseAmount * 0.05m, 2);

        Assert.Equal(expectedPit, pit.Amount);
    }

    [Fact]
    public async Task Should_Calculate_MilitaryTax_Correctly()
    {
        var report = CreateTimeReport(100);
        var results = await _calculator.CalculateAsync(report);

        var militaryTax = results.First(x => x.Type == PaymentRequestType.MilitaryTax);

        var gross = 100 * 10 * 43;
        var baseAmount = gross - 8000 * 0.22m;
        var expectedMilitary = Math.Round(baseAmount * 0.01m, 2);

        Assert.Equal(expectedMilitary, militaryTax.Amount);
    }

    [Fact]
    public async Task Should_Calculate_NetSalary_Correctly()
    {
        var report = CreateTimeReport(100);
        var results = await _calculator.CalculateAsync(report);

        var net = results.First(x => x.Type == PaymentRequestType.NetSalary);

        var gross = 100 * 10 * 43;
        var social = 8000 * 0.22m;
        var baseAmount = gross - social;
        var pit = baseAmount * 0.05m;
        var military = baseAmount * 0.01m;
        var expectedNet = Math.Round(gross - social - pit - military, 2);

        Assert.Equal(expectedNet, net.Amount);
    }

    private TimeReport CreateTimeReport(decimal hours)
    {
        var employee = new Employee
        {
            HourlyRate = 10,
            HourlyRateCurrencyId = 3, // EUR
            SalaryScheme = SalaryScheme.FOP3
        };

        var report = new TimeReport
        {
            Id = Guid.NewGuid(),
            Year = 2025,
            Month = Month.May,
            Status = TimeReportStatus.Approved,
            Employee = employee,
            Activities = [new EmployeeActivity
            {
                Hours = hours,
                Date = DateTime.UtcNow.Date
            }]
        };

        return report;
    }
}
