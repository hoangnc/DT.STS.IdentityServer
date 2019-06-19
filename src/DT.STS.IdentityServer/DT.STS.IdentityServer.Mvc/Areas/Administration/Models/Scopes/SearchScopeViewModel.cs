using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DT.STS.IdentityServer.Mvc.Areas.Administration.Models.Scopes
{
    public class SearchScopeViewModel
    {
        public int Id { get; set; }
        public bool Enabled { get; set; }
        public string Name { get; set; }
        public string DisplayName { get; set; }
        public string Description { get; set; }
        public string Type { get; set; }
        public bool Required { get; set; }
        public bool Emphasize { get; set; }
        public bool IncludeAllClaimsForUser { get; set; }
        public string ClaimsRule { get; set; }
        public bool ShowInDiscoveryDocument { get; set; }
        public string ScopeClaims { get; set; }
    }
}