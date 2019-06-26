namespace DT.STS.IdentityServer.Domain.Entities
{
    public class UserClaim : BaseEntity
    {
        public int UserId { get; set; }
        public int ClaimId { get; set; }
    }
}
