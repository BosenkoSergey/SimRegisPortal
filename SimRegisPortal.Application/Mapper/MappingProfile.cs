using AutoMapper;
using SimRegisPortal.Application.Models.Auth.Responses;
using SimRegisPortal.Core.Entities;

namespace SimRegisPortal.Application.Mapper;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        #region Requests



        #endregion

        #region Responses

        CreateMap<UserSession, AuthResponse>()
            .ForMember(dest => dest.IsAdmin, opt => opt.MapFrom(src => src.User.IsAdmin))
            .ForMember(dest => dest.Permissions, opt =>
                opt.MapFrom(src => src.User.Permissions.Select(p => p.PermissionType).ToList()));

        #endregion
    }
}
