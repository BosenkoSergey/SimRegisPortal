using Microsoft.EntityFrameworkCore;
using SimRegisPortal.Application.Features.Base.Queries;
using SimRegisPortal.Core.Entities;
using SimRegisPortal.Persistence.Context;

namespace SimRegisPortal.Application.Features.Employees.Queries;

public sealed record GetEmployeesLookupQuery
    : GetLookupQuery<Guid>;

internal sealed class GetEmployeesLookupHandler(AppDbContext dbContext)
    : GetLookupHandler<GetEmployeesLookupQuery, Employee, Guid>(dbContext)
{
    protected override async Task<Dictionary<Guid, string>> GetLookupEntities()
    {
        return await Repository
            .ToDictionaryAsync(e => e.Id, e => $"{e.LastName} {e.FirstName} {e.MiddleName}");
    }
}
