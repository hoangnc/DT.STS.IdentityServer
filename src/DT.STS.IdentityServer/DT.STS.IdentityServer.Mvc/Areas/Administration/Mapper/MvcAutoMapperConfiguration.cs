using AutoMapper;
using DT.STS.IdentityServer.Application.Clients.Commands;
using DT.STS.IdentityServer.Application.Clients.Queries;
using DT.STS.IdentityServer.Application.Scopes.Commands;
using DT.STS.IdentityServer.Application.Scopes.Queries;
using DT.STS.IdentityServer.Application.Users.Commands;
using DT.STS.IdentityServer.Mvc.Areas.Administration.Models.Clients;
using DT.STS.IdentityServer.Mvc.Areas.Administration.Models.Scopes;
using DT.STS.IdentityServer.Mvc.Areas.Administration.Models.Users;
using System.Linq;

namespace DT.STS.IdentityServer.Mvc.Areas.Administration.Mapper
{
    public static class MvcAutoMapperConfiguration
    {
        public static void Initialize()
        {
            MapperConfiguration = new MapperConfiguration(cfg =>
            {
                // ----- CreateUserCommand -----
                cfg.CreateMap<UserCreateModel, CreateUserCommand>();

                // ----- CreateScopeCommand -----
                cfg.CreateMap<ScopeCreateModel, CreateScopeCommand>();

                // ----- UpdateScopeCommand -----
                cfg.CreateMap<ScopeUpdateModel, UpdateScopeCommand>();

                // ----- SearchScopeViewModel -----
                cfg.CreateMap<SearchScopesByTokenPagedDto, SearchScopeViewModel>()
                .ForMember(dest => dest.Type,
                    mo => mo.MapFrom(src => src.Type == 0 ? "Identity" : "Resource"))
                .ForMember(dest => dest.ScopeClaims,
                    mo => mo.MapFrom(src => string.Join(";", src.ScopeClaims.Select(s => s.Description))));

                // ----- ScopeUpdateModel -----
                cfg.CreateMap<GetScopeByIdDto, ScopeUpdateModel>()
                .ForMember(dest => dest.ScopeClaims,
                    mo => mo.MapFrom(src => src.ScopeClaims.Select(sc=>sc.Id)));

                // ----- SearchClientViewModel -----
                cfg.CreateMap<SearchClientsByTokenPagedDto, SearchClientViewModel>();

                // ----- CreateClientCommand -----
                cfg.CreateMap<ClientCreateModel, CreateClientCommand>()
                .ForMember(dest => dest.Scopes,
                  mo=>mo.MapFrom(src => string.Join(";", src.Scopes)));

                // ----- ClientUpdateModel -----
                cfg.CreateMap<GetClientByClientIdDto, ClientUpdateModel>()
                .ForMember(dest => dest.Scopes,
                   mo => mo.MapFrom(src => src.Scopes.Split(';')));
            });
            Mapper = MapperConfiguration.CreateMapper();
        }
        public static IMapper Mapper { get; private set; }

        public static MapperConfiguration MapperConfiguration { get; private set; }
    }
}