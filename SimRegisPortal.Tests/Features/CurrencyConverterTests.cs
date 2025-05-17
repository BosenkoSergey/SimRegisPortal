using SimRegisPortal.Application.Services;
using SimRegisPortal.Core.Entities;
using SimRegisPortal.Core.Exceptions;
using SimRegisPortal.Persistence.Context;
using SimRegisPortal.Tests.Context;

namespace SimRegisPortal.Tests.Features;

public class CurrencyConverterTests
{
    private readonly AppDbContext _context;
    private readonly CurrencyConverter _converter;

    public CurrencyConverterTests()
    {
        _context = InMemoryDbContextFactory.CreateContext();
        _converter = new CurrencyConverter(_context);
    }

    [Fact]
    public async Task Should_Convert_UsingDirectRate()
    {
        var result = await _converter.ConvertAsync(100, 2, 1); // USD -> UAH
        Assert.Equal(4000.00m, result); // 100 * 40
    }

    [Fact]
    public async Task Should_Convert_UsingInverseRate()
    {
        var result = await _converter.ConvertAsync(100, 3, 1); // EUR -> UAH (no direct, uses inverse)
        Assert.Equal(4300.00m, result);
    }

    [Fact]
    public async Task Should_Throw_NoRateFound()
    {
        await Assert.ThrowsAsync<CommonException>(() =>
            _converter.ConvertAsync(100, 3, 2)); // EUR -> USD (rate not seeded)
    }
}
