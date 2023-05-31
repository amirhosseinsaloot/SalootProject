﻿using Api.Dtos.Role;
using Api.Dtos.User;
using AutoMapper;
using Core.Entities.Identity;

namespace Api.AutoMapperConfiguration.Profiles.Authentication;

public class AuthenticationProfiles : Profile
{
    public AuthenticationProfiles()
    {
        // User mappings
        CreateMap<UserCreateDto, User>()
           .ForMember(dest => dest.Id, src => src.Ignore())
           .ForMember(dest => dest.IsActive, src => src.MapFrom(s => true))
           .ForMember(dest => dest.ConcurrencyStamp, src => src.Ignore())
           .ForMember(dest => dest.SecurityStamp, src => src.Ignore())
           .ForMember(dest => dest.AccessFailedCount, src => src.Ignore())
           .ForMember(dest => dest.LastLoginDate, src => src.Ignore())
           .ForMember(dest => dest.LockoutEnabled, src => src.Ignore())
           .ForMember(dest => dest.LockoutEnd, src => src.Ignore())
           .ForMember(dest => dest.NormalizedEmail, src => src.Ignore())
           .ForMember(dest => dest.EmailConfirmed, src => src.Ignore())
           .ForMember(dest => dest.NormalizedUserName, src => src.Ignore())
           .ForMember(dest => dest.PasswordHash, src => src.Ignore())
           .ForMember(dest => dest.PhoneNumberConfirmed, src => src.Ignore())
           .ForMember(dest => dest.RefreshToken, src => src.Ignore())
           .ForMember(dest => dest.RefreshTokenExpirationTime, src => src.Ignore())
           .ForMember(dest => dest.TwoFactorEnabled, src => src.Ignore());
        CreateMap<UserUpdateDto, User>()
            .ForMember(dest => dest.Id, src => src.Ignore())
            .ForMember(dest => dest.ConcurrencyStamp, src => src.Ignore())
            .ForMember(dest => dest.SecurityStamp, src => src.Ignore())
            .ForMember(dest => dest.AccessFailedCount, src => src.Ignore())
            .ForMember(dest => dest.LastLoginDate, src => src.Ignore())
            .ForMember(dest => dest.LockoutEnabled, src => src.Ignore())
            .ForMember(dest => dest.LockoutEnd, src => src.Ignore())
            .ForMember(dest => dest.NormalizedEmail, src => src.Ignore())
            .ForMember(dest => dest.EmailConfirmed, src => src.Ignore())
            .ForMember(dest => dest.NormalizedUserName, src => src.Ignore())
            .ForMember(dest => dest.PasswordHash, src => src.Ignore())
            .ForMember(dest => dest.PhoneNumberConfirmed, src => src.Ignore())
            .ForMember(dest => dest.RefreshToken, src => src.Ignore())
            .ForMember(dest => dest.RefreshTokenExpirationTime, src => src.Ignore())
            .ForMember(dest => dest.TwoFactorEnabled, src => src.Ignore());
        CreateMap<User, UserDto>();
        CreateMap<User, UserListDto>();

        // Role mappings
        CreateMap<RoleCreateUpdateDto, Role>()
            .ForMember(dest => dest.Id, src => src.Ignore())
            .ForMember(dest => dest.ConcurrencyStamp, src => src.Ignore())
            .ForMember(dest => dest.NormalizedName, src => src.Ignore());
        CreateMap<Role, RoleDto>();
        CreateMap<Role, RoleListDto>();
    }
}