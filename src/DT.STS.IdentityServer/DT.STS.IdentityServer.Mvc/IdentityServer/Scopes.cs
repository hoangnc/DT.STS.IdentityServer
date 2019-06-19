using IdentityServer3.Core.Models;
using System.Collections.Generic;

namespace DT.STS.IdentityServer.Mvc.IdentityServer
{
    public static class Scopes
    {
        public static IEnumerable<Scope> Get()
        {
            var scopes = new List<Scope>
            {
                new Scope
                {
                    Enabled = true,
                    Name = "roles",
                    Type = ScopeType.Identity,
                    Claims = new List<ScopeClaim>
                    {
                        new ScopeClaim("role")
                    }
                },
                new Scope
                {
                    Enabled = true,
                    DisplayName = "IdentityServer API",
                    Name = "adminapi",
                    Description = "Access to a IdentityServer API",
                    Type = ScopeType.Resource,
                    Claims = new List<ScopeClaim>
                    {
                        new ScopeClaim("role")
                    }
                }
            };
            
            scopes.AddRange(StandardScopes.All);

            return scopes;
        }
    }
}