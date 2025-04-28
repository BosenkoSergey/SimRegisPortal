using AutoMapper;
using SimRegisPortal.Application.Features.Base.Queries;
using SimRegisPortal.Application.Models.Entities;
using SimRegisPortal.Core.Entities;
using SimRegisPortal.Persistence.Context;

namespace SimRegisPortal.Application.Features.Projects.Queries;

public sealed record GetProjectsQuery
    : GetManyQuery<ProjectDto>;

internal sealed class GetProjectsHandler(AppDbContext dbContext, IMapper mapper)
    : GetManyHandler<GetProjectsQuery, Project, ProjectDto>(dbContext, mapper);
