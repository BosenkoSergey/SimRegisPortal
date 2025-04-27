using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SimRegisPortal.Application.Models.Base;
using SimRegisPortal.Core.Entities.Base;
using SimRegisPortal.Core.Exceptions;
using SimRegisPortal.Persistence.Context;

namespace SimRegisPortal.Application.Features.Base.Commands;

public abstract record SaveCommand<TDto, TKey>(TDto Dto)
    : IRequest<TDto>
    where TDto : BaseEntityDto<TKey>;

internal abstract class SaveHandler<TCommand, TDto, TEntity, TKey>(AppDbContext dbContext, IMapper mapper)
    : CommandHandler<TEntity>(dbContext), IRequestHandler<TCommand, TDto>
    where TCommand : SaveCommand<TDto, TKey>
    where TEntity : BaseEntity<TKey>
    where TDto : BaseEntityDto<TKey>
{
    protected readonly IMapper Mapper = mapper;

    public async Task<TDto> Handle(TCommand command, CancellationToken cancellationToken)
    {
        TEntity entity;

        if (command.Dto.IsNew)
        {
            entity = await CreateEntity(command);
            await DbSet.AddAsync(entity, cancellationToken);
        }
        else
        {
            entity = await GetEntity(command, cancellationToken);
            await UpdateEntity(entity, command);
        }

        await DbContext.SaveChangesAsync(cancellationToken);
        return CreateResponse(entity);
    }

    protected virtual Task<TEntity> CreateEntity(TCommand command)
    {
        var entity = Mapper.Map<TEntity>(command.Dto);
        return Task.FromResult(entity);
    }

    protected virtual async Task<TEntity> GetEntity(TCommand command, CancellationToken cancellationToken)
    {
        return await GetEntityQuery(command)
                .Where(e => e.Id!.Equals(command.Dto.Id))
                .FirstOrDefaultAsync(cancellationToken)
            ?? throw new ResourceNotFoundException(typeof(TEntity).Name);
    }

    protected virtual IQueryable<TEntity> GetEntityQuery(TCommand command)
    {
        return DbSet;
    }

    protected virtual Task UpdateEntity(TEntity entity, TCommand command)
    {
        Mapper.Map(command.Dto, entity);
        return Task.CompletedTask;
    }

    protected virtual TDto CreateResponse(TEntity entity)
    {
        return Mapper.Map<TDto>(entity);
    }
}