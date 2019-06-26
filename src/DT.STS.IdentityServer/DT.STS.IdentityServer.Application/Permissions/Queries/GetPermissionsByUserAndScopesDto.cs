namespace DT.STS.IdentityServer.Application.Permissions.Queries
{
    public class GetPermissionsByUserAndScopesDto
    {
        public string ScopeName { get; set; }
        public string ClaimName { get; set; }
        public string ClaimValue { get; set; }
    }
}
