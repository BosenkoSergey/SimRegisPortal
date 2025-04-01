using AutoMapper;
using SimRegisPortal.Application.Models.Auth.Responses;
using SimRegisPortal.Domain.Entities;

namespace SimRegisPortal.Application.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            #region Requests



            #endregion

            #region Responses

            CreateMap<UserSession, AuthResponse>();

            #endregion
        }
    }
}
