using AutoMapper;
using SimRegisPortal.Application.Models.Auth;
using SimRegisPortal.Application.Models.Currency;
using SimRegisPortal.Application.Models.ExchangeRate;
using SimRegisPortal.Application.Models.Mailing;
using SimRegisPortal.Application.Models.Mailing.Common;
using SimRegisPortal.Application.Models.User;
using SimRegisPortal.Core.Entities;

namespace SimRegisPortal.Application.Mapper;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        #region Requests

        CreateMap<UserDto, User>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.Permissions, opt => opt.Ignore())
            .AfterMap((src, dest) =>
            {
                dest.UpdatePermissions(src.Permissions);
            });

        CreateMap<ExchangeRateDto, ExchangeRate>()
            .ForMember(dest => dest.Id, opt => opt.Ignore());

        #endregion

        #region Responses

        CreateMap<UserSession, AuthResponse>()
            .ForMember(dest => dest.IsAdmin, opt => opt.MapFrom(src => src.User.IsAdmin))
            .AfterMap((src, dest) =>
            {
                dest.Permissions = src.User.Permissions.Select(p => p.PermissionType).ToList();
            });

        CreateMap<User, UserDto>()
            .ForMember(dest => dest.Permissions, opt => opt.Ignore())
            .AfterMap((src, dest) =>
            {
                dest.Permissions = src.Permissions.Select(al => al.PermissionType).ToList();
            });

        CreateMap<Currency, CurrencyDto>();

        CreateMap<ExchangeRate, ExchangeRateDto>();

        #endregion

        #region Others

        CreateMap<User, RecipientDto>()
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.FullName));

        CreateMap<User, UserCredentialsEmailDto>()
            .ForMember(dest => dest.Recipient, opt => opt.MapFrom(src => src));

        #endregion
    }
}
