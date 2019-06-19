using System.Web.Mvc;
using System.Web.Routing;

namespace DT.STS.IdentityServer.Mvc
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapMvcAttributeRoutes();

            // Dashboard
            routes.MapRoute(
                name: "Dashboard",
                url: "Administration",
                defaults: new { area = "Administration", controller = "Dashboard", action = "Index" });

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
