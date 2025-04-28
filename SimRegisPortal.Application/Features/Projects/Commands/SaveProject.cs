using AutoMapper;
using SimRegisPortal.Application.Features.Base.Commands;
using SimRegisPortal.Application.Models.Entities;
using SimRegisPortal.Core.Entities;
using SimRegisPortal.Persistence.Context;

namespace SimRegisPortal.Application.Features.Projects.Commands;

public sealed record SaveProjectCommand(ProjectDto Dto)
    : SaveCommand<ProjectDto, Guid>(Dto);

internal sealed class SaveProjectHandler(AppDbContext dbContext, IMapper mapper)
    : SaveHandler<SaveProjectCommand, ProjectDto, Project, Guid>(dbContext, mapper);