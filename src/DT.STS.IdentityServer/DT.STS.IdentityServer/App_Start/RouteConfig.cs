﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace DT.STS.IdentityServer
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapMvcAttributeRoutes();

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "home", action = "index", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "IdentityServer",
                url: "identity/{controller}/{action}/{id}",
                defaults: new { controller = "account", action = "login", id = UrlParameter.Optional }
            );
        }
    }
}
