﻿using AutoMapper;
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

        CreateMap<TimeReportDto, TimeReport>()
            .ForMember(dest => dest.Id, opt => opt.Ignore());

        CreateMap<PaymentRequestDto, PaymentRequest>()
            .ForMember(dest => dest.Id, opt => opt.Ignore());

        CreateMap<TaxSettingDto, TaxSetting>()
            .ForMember(dest => dest.Id, opt => opt.Ignore());

        #endregion

        #region Responses

        CreateMap<User, AuthResponse>()
            .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.FullName))
            .ForMember(dest => dest.EmployeeId, opt => opt.MapFrom(src => src.Employee != null ? (Guid?)src.Employee.Id : null))
            .ForMember(dest => dest.Permissions, opt => opt.Ignore())
            .AfterMap((src, dest) =>
            {
                dest.Permissions = src.Permissions.Select(p => p.PermissionType).ToHashSet();
            });

        CreateMap<User, UserDto>()
            .ForMember(dest => dest.Permissions, opt => opt.Ignore())
            .AfterMap((src, dest) =>
            {
                dest.Permissions = src.Permissions.Select(al => al.PermissionType).ToHashSet();
            });

        CreateMap<Currency, CurrencyDto>();

        CreateMap<ExchangeRate, ExchangeRateDto>();

        CreateMap<Company, CompanyDto>();

        CreateMap<Project, ProjectDto>();

        CreateMap<Contract, ContractDto>();

        CreateMap<Employee, EmployeeDto>();

        CreateMap<EmployeeActivity, EmployeeActivityDto>();

        CreateMap<TimeReport, TimeReportDto>();

        CreateMap<PaymentRequest, PaymentRequestDto>()
            .ForMember(dest => dest.Year, opt => opt.MapFrom(src => src.TimeReport.Year))
            .ForMember(dest => dest.Month, opt => opt.MapFrom(src => src.TimeReport.Month))
            .ForMember(dest => dest.EmployeeFullName, opt => opt.MapFrom(src =>
                $"{src.TimeReport.Employee.LastName} {src.TimeReport.Employee.FirstName} {src.TimeReport.Employee.MiddleName}"));

        CreateMap<TaxSetting, TaxSettingDto>();

        CreateMap<SystemLog, SystemLogDto>();

        #endregion

        #region Others

        CreateMap<User, RecipientDto>()
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.FullName));

        CreateMap<User, UserCredentialsEmailDto>()
            .ForMember(dest => dest.Recipient, opt => opt.MapFrom(src => src));

        #endregion
    }
}
