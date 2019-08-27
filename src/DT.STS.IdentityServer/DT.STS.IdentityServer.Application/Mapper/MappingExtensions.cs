using DT.STS.IdentityServer.Application.Departments.Queries;
using DT.STS.IdentityServer.Application.ScopeScopeClaims.Queries;
using DT.STS.IdentityServer.Application.Scopes.Commands;
using DT.STS.IdentityServer.Application.Users.Commands;
using DT.STS.IdentityServer.Application.Users.Queries;
using DT.STS.IdentityServer.Domain.Entities;
using DT.STS.IdentityServer.Application.Scopes.Queries;
using DT.STS.IdentityServer.Application.Clients.Queries;
using DT.STS.IdentityServer.Application.Clients.Commands;
using DT.STS.IdentityServer.Application.UserScopes.Commands;
using DT.STS.IdentityServer.Application.Claims.Queries;
using DT.STS.IdentityServer.Application.Companies.Queries;
using DT.STS.IdentityServer.Application.Departments.Commands;

namespace DT.STS.IdentityServer.Application.Mapper
{
    public  static class MappingExtensions
    {
        public static TDestination MapTo<TSource, TDestination>(this TSource source)
        {
            return AutoMapperConfiguration.Mapper.Map<TSource, TDestination>(source);
        }

        public static TDestination MapTo<TSource, TDestination>(this TSource source, TDestination destination)
        {
            return AutoMapperConfiguration.Mapper.Map(source, destination);
        }

        #region Users
        public static GetUsersPagedDto ToGetUsersPagedDto(this User entity)
        {
            return entity.MapTo<User, GetUsersPagedDto>();
        }
        public static SearchUsersByTokenPagedDto ToSearchUsersByTokenDto(this User entity)
        {
            return entity.MapTo<User, SearchUsersByTokenPagedDto>();
        }
        public static User ToUser(this CreateUserCommand command)
        {
            return command.MapTo<CreateUserCommand, User>();
        }
        public static User ToUser(this UpdateUserCommand command)
        {
            return command.MapTo<UpdateUserCommand, User>();
        }
        public static GetAllUsersDto ToGetAllUsersDto(this User entity)
        {
            return entity.MapTo<User, GetAllUsersDto>();
        }
        public static GetUserByIdDto ToGetUserByIdDto(this User entity)
        {
            return entity.MapTo<User, GetUserByIdDto>();
        }
        #endregion

        #region Departments
        public static GetAllDepartmentsDto ToGetAllDepartmentsDto(this Department entity)
        {
            return entity.MapTo<Department, GetAllDepartmentsDto>();
        }

        public static SearchDepartmentsByTokenPagedDto ToSearchDepartmentsByTokenPagedDto(this Department entity)
        {
            return entity.MapTo<Department, SearchDepartmentsByTokenPagedDto>();
        }

        public static Department ToDepartment(this UpdateDepartmentCommand command)
        {
            return command.MapTo<UpdateDepartmentCommand, Department>();
        }
        public static GetDepartmentByIdDto ToGetDepartmentByIdDto(this Department entity)
        {
            return entity.MapTo<Department, GetDepartmentByIdDto>();
        }
        
        #endregion

        #region ScopeClaims
        public static GetAllScopeClaimsDto ToGetAllScopeClaimsDto(this ScopeClaim entity)
        {
            return entity.MapTo<ScopeClaim, GetAllScopeClaimsDto>();
        }
        #endregion

        #region Scopes
        public static Scope ToScope(this CreateScopeCommand command)
        {
            return command.MapTo<CreateScopeCommand, Scope>();
        }

        public static Scope ToScope(this UpdateScopeCommand command)
        {
            return command.MapTo<UpdateScopeCommand, Scope>();
        }

        public static GetScopeByIdDto ToGetScopeByIdDto(this Scope entity)
        {
            return entity.MapTo<Scope, GetScopeByIdDto>();
        }

        public static GetScopesDto ToGetScopesDto(this Scope entity)
        {
            return entity.MapTo<Scope, GetScopesDto>();
        }
        #endregion

        #region Clients
        public static SearchClientsByTokenPagedDto ToSearchClientsByTokenPagedDto(this Client entity)
        {
            return entity.MapTo<Client, SearchClientsByTokenPagedDto>();
        }

        public static Client ToClient(this CreateClientCommand command)
        {
            return command.MapTo<CreateClientCommand, Client>();
        }

        public static Client ToClient(this UpdateClientCommand command)
        {
            return command.MapTo<UpdateClientCommand, Client>();
        }
        #endregion

        #region UserScopes

        public static UserScope ToUserScope(this InsertOrUpdateUserScopeCommand command)
        {
            return command.MapTo<InsertOrUpdateUserScopeCommand, UserScope>();
        }
        #endregion

        #region Claims
        public static GetAllClaimsDto ToGetAllClaimsDto(this Claim claim)
        {
            return claim.MapTo<Claim, GetAllClaimsDto>();
        }
        #endregion

        #region Companies
        public static GetAllCompaniesDto ToGetAllCompaniesDto(this Company company)
        {
            return company.MapTo<Company, GetAllCompaniesDto>();
        }
        #endregion
    }
}
