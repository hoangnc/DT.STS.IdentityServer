using Abp.Web.Localization;
using System.Net;
using System.Web.Mvc;

namespace DT.STS.IdentityServer.Mvc
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_BeginRequest()
        {
            DependencyResolver.Current.GetService<ICurrentCultureSetter>().SetCurrentCulture(Context);
        }

        protected void Application_Start()
        {
            ServicePointManager.ServerCertificateValidationCallback = delegate
            {
                return true;
            };
        }

        protected void Session_Start() { }
    }
}
