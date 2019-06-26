namespace DT.STS.IdentityServer.Application.Claims.Queries
{
    public class GetAllClaimsDto
    {
        public string Name { get; set; }
        public string Value { get; set; }
        public string Text => $"{Name} {Value}";
        public string Id => $"{Name}_{Value}";
    }
}
