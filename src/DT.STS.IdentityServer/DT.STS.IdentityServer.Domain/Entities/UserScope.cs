namespace DT.STS.IdentityServer.Domain.Entities
{
    public class UserScope : BaseEntity
    {
        public string Users { get; set; }
        public string Permissions { get; set; }
        public string ScopeName { get; set; }
    }
}
