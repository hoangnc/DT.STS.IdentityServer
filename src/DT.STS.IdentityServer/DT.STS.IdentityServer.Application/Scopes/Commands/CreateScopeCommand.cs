using IdentityServer3.Core.Models;
using MediatR;
using System.Collections.Generic;

namespace DT.STS.IdentityServer.Application.Scopes.Commands
{
    public class CreateScopeCommand : BaseCommand, IRequest<int>
    {
        public bool Enabled { get; set; }
        public string Name { get; set; }
        public string DisplayName { get; set; }
        public string Description { get; set; }
        public ScopeType Type { get; set; }
        public bool Required { get; set; }
        public bool Emphasize { get; set; }
        public bool IncludeAllClaimsForUser { get; set; }
        public string ClaimsRule { get; set; }
        public bool ShowInDiscoveryDocument { get; set; }
        public List<int> ScopeClaims { get; set; }
    }
}
