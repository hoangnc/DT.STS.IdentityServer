using AutoMapper;
using DT.STS.IdentityServer.Application.Claims.Queries;
using DT.STS.IdentityServer.Application.Clients.Commands;
using DT.STS.IdentityServer.Application.Clients.Queries;
using DT.STS.IdentityServer.Application.Companies.Queries;
using DT.STS.IdentityServer.Application.Departments.Commands;
using DT.STS.IdentityServer.Application.Departments.Queries;
using DT.STS.IdentityServer.Application.Scopes.Commands;
using DT.STS.IdentityServer.Application.Scopes.Queries;
using DT.STS.IdentityServer.Application.ScopeScopeClaims.Queries;
using DT.STS.IdentityServer.Application.Users.Commands;
using DT.STS.IdentityServer.Application.Users.Queries;
using DT.STS.IdentityServer.Application.UserScopes.Commands;
using DT.STS.IdentityServer.Domain.Entities;
using System;

namespace DT.STS.IdentityServer.Application.Mapper
{
    public static class AutoMapperConfiguration
    {
        public static void Initialize()
        {
            MapperConfiguration = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<CreateUserCommand, User>();

                cfg.CreateMap<UpdateUserCommand, User>();

                cfg.CreateMap<User, GetAllUsersDto>();

                // ----- GetUsersPagedDto -----
                cfg.CreateMap<User, GetUsersPagedDto>()
                .ForMember(dest => dest.JpegPhoto,
                    mo => mo.MapFrom(src => src.JpegPhoto != null ? Convert.ToBase64String(src.JpegPhoto) : string.Empty));

                cfg.CreateMap<GetUsersPagedDto, User>();

                // ----- SearchUserByTokenPagedDto -----
                cfg.CreateMap<User, SearchUsersByTokenPagedDto>()
                .ForMember(dest => dest.JpegPhoto,
                    mo => mo.MapFrom(src => src.JpegPhoto != null ? Convert.ToBase64String(src.JpegPhoto) : string.Empty));

                cfg.CreateMap<User, GetUserByIdDto>()
                .ForMember(dest => dest.JpegPhoto,
                    mo => mo.MapFrom(src => src.JpegPhoto != null ? Convert.ToBase64String(src.JpegPhoto) : string.Empty));

                cfg.CreateMap<SearchUsersByTokenPagedDto, User>();

                // ----- GetAllDeparmentsDto -----
                cfg.CreateMap<Department, GetAllDepartmentsDto>();

                // ----- SearchDepartmentsByTokenPagedDto -----
                cfg.CreateMap<Department, SearchDepartmentsByTokenPagedDto>();

                // ----- GetAllScopeClaimsDto -----
                cfg.CreateMap<ScopeClaim, GetAllScopeClaimsDto>();

                // ----- CreateScopeCommand ------
                cfg.CreateMap<CreateScopeCommand, Scope>();

                // ----- UpdateScopeCommand -----
                cfg.CreateMap<UpdateScopeCommand, Scope>();

                // ------ GetScopeByIdDto -----
                cfg.CreateMap<Scope, GetScopeByIdDto>();

                // ------ GetScopesDto -----
                cfg.CreateMap<Scope, GetScopesDto>();

                // ----- SearchClientsByTokenPagedDto -----
                cfg.CreateMap<Client, SearchClientsByTokenPagedDto>();

                // ----- CreateClientCommand -----
                cfg.CreateMap<CreateClientCommand, Client>();

                // ----- UpdateClientCommand -----
                cfg.CreateMap<UpdateClientCommand, Client>();

                // ----- UserScope -----
                cfg.CreateMap<InsertOrUpdateUserScopeCommand, UserScope>();

                // ----- GetAllClaimsDto -----
                cfg.CreateMap<Claim, GetAllClaimsDto>();

                // ----- GetAllCompaniesDto -----
                cfg.CreateMap<Company, GetAllCompaniesDto>();

                cfg.CreateMap<UpdateDepartmentCommand, Department>();

                cfg.CreateMap<Department, GetDepartmentByIdDto>();

            });
            Mapper = MapperConfiguration.CreateMapper();
        }
        public static IMapper Mapper { get; private set; }

        public static MapperConfiguration MapperConfiguration { get; private set; }
    }
}
