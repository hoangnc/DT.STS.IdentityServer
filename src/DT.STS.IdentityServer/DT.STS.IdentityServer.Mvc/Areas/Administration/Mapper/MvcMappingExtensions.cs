using DT.STS.IdentityServer.Application.Clients.Commands;
using DT.STS.IdentityServer.Application.Clients.Queries;
using DT.STS.IdentityServer.Application.Scopes.Commands;
using DT.STS.IdentityServer.Application.Scopes.Queries;
using DT.STS.IdentityServer.Application.Users.Commands;
using DT.STS.IdentityServer.Mvc.Areas.Administration.Models.Clients;
using DT.STS.IdentityServer.Mvc.Areas.Administration.Models.Scopes;
using DT.STS.IdentityServer.Mvc.Areas.Administration.Models.Users;

namespace DT.STS.IdentityServer.Mvc.Areas.Administration.Mapper
{
    public static class MvcMappingExtensions
    {
        public static TDestination MapTo<TSource, TDestination>(this TSource source)
        {
            return MvcAutoMapperConfiguration.Mapper.Map<TSource, TDestination>(source);
        }

        public static TDestination MapTo<TSource, TDestination>(this TSource source, TDestination destination)
        {
            return MvcAutoMapperConfiguration.Mapper.Map(source, destination);
        }

        public static CreateUserCommand ToCreateUserCommand(this UserCreateModel model)
        {
            return model.MapTo<UserCreateModel, CreateUserCommand>();
        }

        #region Scopes

        public static CreateScopeCommand ToCreateScopeCommand(this ScopeCreateModel model)
        {
            return model.MapTo<ScopeCreateModel, CreateScopeCommand>();
        }

        public static SearchScopeViewModel ToSearchScopeViewModel(this SearchScopesByTokenPagedDto dto)
        {
            return dto.MapTo<SearchScopesByTokenPagedDto, SearchScopeViewModel>();
        }

        public static ScopeUpdateModel ToScopeUpdateModel(this GetScopeByIdDto dto)
        {
            return dto.MapTo<GetScopeByIdDto, ScopeUpdateModel>();
        }

        public static UpdateScopeCommand ToUpdateScopeCommand(this ScopeUpdateModel model)
        {
            return model.MapTo<ScopeUpdateModel, UpdateScopeCommand>();
        }
        #endregion
        #region Clients
        public static SearchClientViewModel ToSearchClientViewModel(this SearchClientsByTokenPagedDto dto)
        {
            return dto.MapTo<SearchClientsByTokenPagedDto, SearchClientViewModel>();
        }
        public static CreateClientCommand ToCreateClientCommand(this ClientCreateModel model)
        {
            return model.MapTo<ClientCreateModel, CreateClientCommand>();
        }

        public static ClientUpdateModel ToClientUpdateModel(this GetClientByClientIdDto dto)
        {
            return dto.MapTo<GetClientByClientIdDto, ClientUpdateModel>();
        }
        #endregion

    }
}