namespace DT.STS.IdentityServer.Domain.Entities
{
    public class Claim : BaseEntity
    {
        public string Name { get; set; }
        public string Value { get; set; }
    }
}
