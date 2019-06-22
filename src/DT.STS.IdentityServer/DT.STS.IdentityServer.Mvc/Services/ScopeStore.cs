using DT.STS.IdentityServer.Application.Scopes.Queries;
using IdentityServer3.Core.Models;
using IdentityServer3.Core.Services;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DT.STS.IdentityServer.Mvc.Services
{
    public class ScopeStore : IScopeStore
    {
        private readonly IMediator _mediator;

        public ScopeStore(IMediator mediator)
        {
            _mediator = mediator;
        }
        public async Task<IEnumerable<Scope>> FindScopesAsync(IEnumerable<string> scopeNames)
        {
            List<BulkGetScopesDto> scopes = await _mediator.Send(new BulkGetScopesQuery {
                ScopeNames = scopeNames
            });

            var result = scopes.Select(scope => new Scope
            {
                Name = scope.Name,
                DisplayName = scope.DisplayName,
                Description = scope.Description,
                Enabled = true,
                Emphasize = scope.Emphasize,
                IncludeAllClaimsForUser = scope.IncludeAllClaimsForUser,
                Required = scope.Required,
                ShowInDiscoveryDocument = scope.ShowInDiscoveryDocument,
                Type = scope.Type,
                Claims = scope.ScopeClaims.Select(scopeClaim => new ScopeClaim
                {
                    AlwaysIncludeInIdToken = scopeClaim.AlwaysIncludeInIdToken,
                    Description = scopeClaim.Description,
                    Name = scopeClaim.Name
                }).ToList()
            }).ToList();

            result.AddRange(StandardScopes.All);

            return result;
        }

        public async Task<IEnumerable<Scope>> GetScopesAsync(bool publicOnly = true)
        {
            List<GetScopesDto> scopes = await _mediator.Send(new GetScopesQuery());
            var result = scopes.Select(scope => new Scope
            {
                Name = scope.Name,
                DisplayName = scope.DisplayName,
                Description = scope.Description,
                Enabled = true,
                Emphasize = scope.Emphasize,
                IncludeAllClaimsForUser = scope.IncludeAllClaimsForUser,
                Required = scope.Required,
                ShowInDiscoveryDocument = scope.ShowInDiscoveryDocument,
                Type = scope.Type,
                Claims = scope.ScopeClaims.Select(scopeClaim => new ScopeClaim
                {
                    AlwaysIncludeInIdToken = scopeClaim.AlwaysIncludeInIdToken,
                    Description = scopeClaim.Description,
                    Name = scopeClaim.Name
                }).ToList()
            }).ToList();

            result.AddRange(StandardScopes.All);

            return result;
        }
    }
}