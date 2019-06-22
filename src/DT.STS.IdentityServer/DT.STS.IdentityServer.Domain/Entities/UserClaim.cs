namespace DT.STS.IdentityServer.Domain.Entities
{
    public class UserClaim : BaseEntity
    {
        public int UserId { get; set; }
        public int ScopeClaimId { get; set; }
    }
}
