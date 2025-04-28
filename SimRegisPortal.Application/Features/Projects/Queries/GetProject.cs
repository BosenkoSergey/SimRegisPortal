using AutoMapper;
using SimRegisPortal.Application.Features.Base.Queries;
using SimRegisPortal.Application.Models.Entities;
using SimRegisPortal.Core.Entities;
using SimRegisPortal.Persistence.Context;

namespace SimRegisPortal.Application.Features.Projects.Queries;

public sealed record GetProjectQuery(Guid Id)
    : GetByIdQuery<Guid, ProjectDto>(Id);

internal sealed class GetProjectHandler(AppDbContext dbContext, IMapper mapper)
    : GetByIdHandler<GetProjectQuery, Project, Guid, ProjectDto>(dbContext, mapper);