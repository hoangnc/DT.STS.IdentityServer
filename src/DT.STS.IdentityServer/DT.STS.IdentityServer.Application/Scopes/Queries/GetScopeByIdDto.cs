using DT.STS.IdentityServer.Domain.Entities;
using Id3Model = IdentityServer3.Core.Models;
using System.Collections.Generic;

namespace DT.STS.IdentityServer.Application.Scopes.Queries
{
    public class GetScopeByIdDto
    {
        public int Id { get; set; }
        public bool Enabled { get; set; }
        public string Name { get; set; }
        public string DisplayName { get; set; }
        public string Description { get; set; }
        public Id3Model.ScopeType Type { get; set; }
        public bool Required { get; set; }
        public bool Emphasize { get; set; }
        public bool IncludeAllClaimsForUser { get; set; }
        public string ClaimsRule { get; set; }
        public bool ShowInDiscoveryDocument { get; set; }
        public ICollection<ScopeClaim> ScopeClaims { get; set; }
    }
}
