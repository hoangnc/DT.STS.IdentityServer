using DT.STS.IdentityServer.Application.Mapper;
using DT.STS.IdentityServer.Mvc.Areas.Administration.Mapper;
using Newtonsoft.Json.Serialization;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace DT.STS.IdentityServer.Mvc
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AutoMapperConfiguration.Initialize();
            MvcAutoMapperConfiguration.Initialize();
            
        }
    }
}
