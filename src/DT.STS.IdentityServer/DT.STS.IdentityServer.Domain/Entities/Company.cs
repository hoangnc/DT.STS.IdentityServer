namespace DT.STS.IdentityServer.Domain.Entities
{
    public class Company : BaseEntity
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public string ShortName { get; set; }
        public string Description { get; set; }
    }
}
