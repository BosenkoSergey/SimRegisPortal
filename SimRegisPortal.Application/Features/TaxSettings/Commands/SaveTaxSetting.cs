using AutoMapper;
using SimRegisPortal.Application.Features.Base.Commands;
using SimRegisPortal.Application.Models.Entities;
using SimRegisPortal.Core.Entities;
using SimRegisPortal.Persistence.Context;

namespace SimRegisPortal.Application.Features.TaxSettings.Commands;

public sealed record SaveTaxSettingCommand(TaxSettingDto Dto)
    : SaveCommand<TaxSettingDto, int>(Dto);

internal sealed class SaveTaxSettingHandler(AppDbContext dbContext, IMapper mapper)
    : SaveHandler<SaveTaxSettingCommand, TaxSettingDto, TaxSetting, int>(dbContext, mapper);