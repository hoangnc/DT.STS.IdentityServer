using Autofac;
using IdentityServer3.Core.Services;
using Microsoft.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DT.STS.IdentityServer.Mvc.Extensions
{
    public static class DependencyResolverExtensions
    {
        public static T GetInstanceFromApplicationDependencyResolver<T>(this IdentityServer3.Core.Services.IDependencyResolver resolver)
        {
            var owin = resolver.Resolve<OwinEnvironmentService>();
            var context = new OwinContext(owin.Environment);
            var autofacOwinLifeTimeScopes = context.Environment.Where(en => en.Key.Contains("autofac:OwinLifetimeScope"));
            var autofacScope = autofacOwinLifeTimeScopes.FirstOrDefault(en => !en.Key.Contains("idsvr"));
            var scope = context.Get<Autofac.Core.Lifetime.LifetimeScope>(autofacScope.Key);
            return scope.Resolve<T>();
        }
    }
}