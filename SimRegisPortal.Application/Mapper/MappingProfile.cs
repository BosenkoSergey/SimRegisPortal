using AutoMapper;
using SimRegisPortal.Application.Models.Auth;
using SimRegisPortal.Application.Models.ExchangeRate;
using SimRegisPortal.Application.Models.Mailing;
using SimRegisPortal.Application.Models.Mailing.Common;
using SimRegisPortal.Application.Models.Users;
using SimRegisPortal.Core.Entities;

namespace SimRegisPortal.Application.Mapper;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        #region Requests

        CreateMap<UserRequest, User>()
            .ForMember(dest => dest.Permissions, opt => opt.Ignore())
            .AfterMap((src, dest) =>
            {
                dest.UpdatePermissions(src.Permissions);
            });

        CreateMap<ExchangeRateRequest, ExchangeRate>();

        #endregion

        #region Responses

        CreateMap<UserSession, AuthResponse>()
            .ForMember(dest => dest.IsAdmin, opt => opt.MapFrom(src => src.User.IsAdmin))
            .AfterMap((src, dest) =>
            {
                dest.Permissions = src.User.Permissions.Select(p => p.PermissionType).ToArray();
            });

        CreateMap<User, UserResponse>()
            .ForMember(dest => dest.Permissions, opt => opt.Ignore())
            .AfterMap((src, dest) =>
            {
                dest.Permissions = src.Permissions.Select(al => al.PermissionType).ToArray();
            });

        CreateMap<ExchangeRate, ExchangeRateResponse>();

        #endregion

        #region Others

        CreateMap<User, RecipientDto>()
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.FullName));

        CreateMap<User, UserCredentialsEmailDto>()
            .ForMember(dest => dest.Recipient, opt => opt.MapFrom(src => src));

        #endregion
    }
}
