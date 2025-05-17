using SimRegisPortal.Application.Services.SalaryCalculators;
using SimRegisPortal.Application.Services;
using SimRegisPortal.Core.Entities;
using SimRegisPortal.Core.Enums;
using SimRegisPortal.Persistence.Context;
using SimRegisPortal.Tests.Context;

namespace SimRegisPortal.Tests.Features;

public class Fop2SalaryCalculatorTests
{
    private readonly AppDbContext _context;
    private readonly Fop2SalaryCalculator _calculator;

    public Fop2SalaryCalculatorTests()
    {
        _context = InMemoryDbContextFactory.CreateContext();
        var converter = new CurrencyConverter(_context);
        _calculator = new Fop2SalaryCalculator(_context, converter);
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

        Assert.Equal(1600.00m, pit.Amount); // 8000 * 20%
    }

    [Fact]
    public async Task Should_Calculate_MilitaryTax_Correctly()
    {
        var report = CreateTimeReport(100);
        var results = await _calculator.CalculateAsync(report);

        var militaryTax = results.First(x => x.Type == PaymentRequestType.MilitaryTax);

        Assert.Equal(800.00m, militaryTax.Amount); // 8000 * 10%
    }

    [Fact]
    public async Task Should_Calculate_NetSalary_Correctly()
    {
        var report = CreateTimeReport(100);
        var results = await _calculator.CalculateAsync(report);

        var net = results.First(x => x.Type == PaymentRequestType.NetSalary);

        var expectedGross = 100 * 10 * 40;
        var expectedSocial = 8000 * 0.22m;
        var expectedPIT = 8000 * 0.20m;
        var expectedMilitary = 8000 * 0.10m;
        var expectedNet = expectedGross - expectedSocial - expectedPIT - expectedMilitary;

        Assert.Equal(expectedNet, net.Amount);
    }

    private TimeReport CreateTimeReport(decimal hours)
    {
        var employee = new Employee
        {
            HourlyRate = 10,
            HourlyRateCurrencyId = 2, // USD
            SalaryScheme = SalaryScheme.FOP2
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
