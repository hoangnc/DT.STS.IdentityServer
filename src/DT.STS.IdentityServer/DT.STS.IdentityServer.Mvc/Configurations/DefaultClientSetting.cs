using DT.STS.IdentityServer.Common.Helpers;
using IdentityServer3.Core.Models;
using System;
using System.Collections.Generic;
using System.Configuration;

namespace DT.STS.IdentityServer.Mvc.Configurations
{
    public class DefaultClientSetting : ConfigurationElement
    {
        public DefaultClientSetting() { }

        [ConfigurationProperty("clientId", IsRequired = true, IsKey = true, DefaultValue = "dtwebappclient")]
        public string ClientId
        {
            get => this["clientId"].ToString();
            set => this["clientId"] = value;
        }

        [ConfigurationProperty("clientName", DefaultValue = "IdentityServer Client")]
        public string ClientName
        {
            get => this["clientName"].ToString();
            set => this["clientName"] = value;
        }

        [ConfigurationProperty("flow", DefaultValue = Flows.Implicit)]
        public Flows Flow
        {
            get => (Flows)this["flow"];
            set => this["flow"] = value;
        }

        [ConfigurationProperty("requireConsent", DefaultValue = false)]
        public bool RequireConsent
        {
            get => Convert.ToBoolean(this["requireConsent"]);
            set => this["requireConsent"] = value;
        }

        [ConfigurationProperty("redirectUris", DefaultValue = "https://localhost:44319/;https://localhost:44319/administration/dashboard")]
        public string RedirectUris
        {
            get => Convert.ToString(this["redirectUris"]);
            set => this["redirectUris"] = value;
        }

        [ConfigurationProperty("postLogoutRedirectUris", DefaultValue = "https://localhost:44319/")]
        public string PostLogoutRedirectUris
        {
            get => Convert.ToString(this["postLogoutRedirectUris"]);
            set => this["postLogoutRedirectUris"] = value;
        }

        [ConfigurationProperty("allowedScopes", DefaultValue = "openid;profile;roles;adminapi")]
        public string AllowedScopes
        {
            get => Convert.ToString(this["allowedScopes"]);
            set => this["allowedScopes"] = value;
        }
    }
}