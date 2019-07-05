using System.Linq;
using System.Threading.Tasks;
using Thinktecture.IdentityModel.Owin.ResourceAuthorization;
using static DT.Core.Web.Common.Identity.Constants;

namespace DT.STS.IdentityServer.Mvc
{
    public static class IdentityServerResources
    {
        public const string Users = "Users";
        public const string ApiUsers = "ApiUsers";
        public const string Scopes = "Scopes";
        public const string ApiScopes = "ApiScopes";
        public const string Clients = "Clients";
        public const string ApiClients = "ApiClients";
        public const string Claims = "Claims";
        public const string ApiClaims = "ApiClaims";
    }

    public class AuthorizationManager : ResourceAuthorizationManager
    {
        private const string AdminMvcClaimType = "adminmvc_permission";
        private const string AdminApiClaimType = "adminapi_permission";
        public override Task<bool> CheckAccessAsync(ResourceAuthorizationContext context)
        {
            switch (context.Resource.First().Value)
            {
                case IdentityServerResources.Users:
                    return AuthorizeUsers(context);
                case IdentityServerResources.Scopes:
                    return AuthorizeScopes(context);
                case IdentityServerResources.Clients:
                    return AuthorizeClients(context);
                case IdentityServerResources.ApiUsers:
                    return AuthorizeApiUsers(context);
                default:
                    return Nok();
            }
        }

        #region Users
        private Task<bool> AuthorizeUsers(ResourceAuthorizationContext context)
        {
            switch (context.Action.First().Value)
            {
                case DtPermissionBaseTypes.Read:
                    return Eval(context.Principal.HasClaim(AdminMvcClaimType, DtPermissionBaseTypes.Read));
                case DtPermissionBaseTypes.Write:
                    return Eval(context.Principal.HasClaim(AdminMvcClaimType, DtPermissionBaseTypes.Write));
                default:
                    return Nok();
            }
        }

        private Task<bool> AuthorizeApiUsers(ResourceAuthorizationContext context)
        {
            switch (context.Action.First().Value)
            {
                case DtPermissionBaseTypes.Read:
                    return Eval(context.Principal.HasClaim(AdminApiClaimType, DtPermissionBaseTypes.Read));
                case DtPermissionBaseTypes.Write:
                    return Eval(context.Principal.HasClaim(AdminApiClaimType, DtPermissionBaseTypes.Write));
                case DtPermissionBaseTypes.Sync:
                    return Eval(context.Principal.HasClaim(AdminApiClaimType, DtPermissionBaseTypes.Sync));
                default:
                    return Nok();
            }
        }
        #endregion
        #region Scopes
        private Task<bool> AuthorizeScopes(ResourceAuthorizationContext context)
        {
            switch (context.Action.First().Value)
            {
                case DtPermissionBaseTypes.Read:
                    return Eval(context.Principal.HasClaim(AdminMvcClaimType, DtPermissionBaseTypes.Read));
                case DtPermissionBaseTypes.Update:
                    return Eval(context.Principal.HasClaim(AdminMvcClaimType, DtPermissionBaseTypes.Update));
                case DtPermissionBaseTypes.Write:
                    return Eval(context.Principal.HasClaim(AdminMvcClaimType, DtPermissionBaseTypes.Write));
                default:
                    return Nok();
            }
        }
        private Task<bool> AuthorizeApiScopes(ResourceAuthorizationContext context)
        {
            switch (context.Action.First().Value)
            {
                case DtPermissionBaseTypes.Read:
                    return Eval(context.Principal.HasClaim(AdminApiClaimType, DtPermissionBaseTypes.Read));
                case DtPermissionBaseTypes.Update:
                    return Eval(context.Principal.HasClaim(AdminApiClaimType, DtPermissionBaseTypes.Update));
                case DtPermissionBaseTypes.Write:
                    return Eval(context.Principal.HasClaim(AdminApiClaimType, DtPermissionBaseTypes.Write));
                default:
                    return Nok();
            }
        }
        #endregion

        #region Clients
        private Task<bool> AuthorizeClients(ResourceAuthorizationContext context)
        {
            switch (context.Action.First().Value)
            {
                case DtPermissionBaseTypes.Read:
                    return Eval(context.Principal.HasClaim(AdminMvcClaimType, DtPermissionBaseTypes.Read));
                case DtPermissionBaseTypes.Write:
                    return Eval(context.Principal.HasClaim(AdminMvcClaimType, DtPermissionBaseTypes.Write));
                default:
                    return Nok();
            }
        }
        private Task<bool> AuthorizeApiClients(ResourceAuthorizationContext context)
        {
            switch (context.Action.First().Value)
            {
                case DtPermissionBaseTypes.Read:
                    return Eval(context.Principal.HasClaim(AdminApiClaimType, DtPermissionBaseTypes.Read));
                case DtPermissionBaseTypes.Write:
                    return Eval(context.Principal.HasClaim(AdminApiClaimType, DtPermissionBaseTypes.Write));
                default:
                    return Nok();
            }
        }
        #endregion
    }
}