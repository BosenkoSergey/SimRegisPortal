using AutoMapper;
using SimRegisPortal.Application.Models.Auth;
using SimRegisPortal.Application.Models.Entities;
using SimRegisPortal.Application.Models.Mailing;
using SimRegisPortal.Application.Models.Mailing.Common;
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

        CreateMap<CompanyDto, Company>()
            .ForMember(dest => dest.Id, opt => opt.Ignore());

        CreateMap<ProjectDto, Project>()
            .ForMember(dest => dest.Id, opt => opt.Ignore());

        CreateMap<ContractDto, Contract>()
            .ForMember(dest => dest.Id, opt => opt.Ignore());

        CreateMap<EmployeeDto, Employee>()
            .ForMember(dest => dest.Id, opt => opt.Ignore());

        CreateMap<EmployeeActivityDto, EmployeeActivity>()
            .ForMember(dest => dest.Id, opt => opt.Ignore());

        CreateMap<TaxSettingDto, TaxSetting>()
            .ForMember(dest => dest.Id, opt => opt.Ignore());

        #endregion

        #region Responses

        CreateMap<User, AuthResponse>()
            .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.Id))
            .AfterMap((src, dest) =>
            {
                dest.Permissions = src.Permissions.Select(p => p.PermissionType).ToHashSet();
            });

        CreateMap<User, UserDto>()
            .ForMember(dest => dest.Permissions, opt => opt.Ignore())
            .AfterMap((src, dest) =>
            {
                dest.Permissions = src.Permissions.Select(al => al.PermissionType).ToList();
            });

        CreateMap<Currency, CurrencyDto>();

        CreateMap<ExchangeRate, ExchangeRateDto>();

        CreateMap<Company, CompanyDto>();

        CreateMap<Project, ProjectDto>();

        CreateMap<Contract, ContractDto>();

        CreateMap<Employee, EmployeeDto>();

        CreateMap<EmployeeActivity, EmployeeActivityDto>();

        CreateMap<TaxSetting, TaxSettingDto>();

        #endregion

        #region Others

        CreateMap<User, RecipientDto>()
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.FullName));

        CreateMap<User, UserCredentialsEmailDto>()
            .ForMember(dest => dest.Recipient, opt => opt.MapFrom(src => src));

        #endregion
    }
}
