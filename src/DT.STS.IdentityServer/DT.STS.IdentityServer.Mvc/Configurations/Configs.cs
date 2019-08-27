using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace DT.STS.IdentityServer.Mvc.Configurations
{
    public static class Configs
    {
        public static bool WindowAuth = Convert.ToBoolean(ConfigurationManager.AppSettings["WindowAuth"]);
    }
}