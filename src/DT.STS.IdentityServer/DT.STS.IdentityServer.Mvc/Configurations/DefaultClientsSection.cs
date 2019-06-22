using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace DT.STS.IdentityServer.Mvc.Configurations
{
    public class DefaultClientsSection : ConfigurationSection
    {
        [ConfigurationProperty("defaultClientSettings")]
        public DefaultClientSettings DefaultClientSettings
        {
            get { return (DefaultClientSettings)this["defaultClientSettings"]; }
        }
    }
}