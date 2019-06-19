namespace DT.STS.IdentityServer.Domain.Entities
{
    public class ScopeClaim : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public bool AlwaysIncludeInIdToken { get; set; }
    }
}
