﻿using Autofac;
using Autofac.Integration.Mvc;
using Autofac.Integration.WebApi;
using DT.STS.IdentityServer.Application;
using DT.STS.IdentityServer.Persistence;
using DT.STS.IdentityServer.Services;
using MediatR;
using System.Configuration;
using System.Reflection;
using System.Web.Http;
using System.Web.Mvc;

namespace DT.STS.IdentityServer
{
    public class AutofacConfig
    {
        public static IContainer ConfigureContainer()
        {
            ContainerBuilder builder = new ContainerBuilder();

            // Register our Persistence dependencies
            builder.RegisterModule(new STSIdentityServerPersistenceModule(ConfigurationManager.ConnectionStrings["StsConnectionString"].ConnectionString));

            // Register out Application dependencies
            builder.RegisterModule(new STSIdentityServerApplicationModule());

            // Register out Application dependencies
            builder.RegisterModule(new STSIdentityServerModule());

            // Get your HttpConfiguration.
            HttpConfiguration config = GlobalConfiguration.Configuration;

            // Register dependencies in controllers
            builder.RegisterControllers(typeof(MvcApplication).Assembly);

            // Register dependencies in filter attributes
            builder.RegisterFilterProvider();

            // Register your Web API controllers.
            builder.RegisterApiControllers(typeof(AutofacConfig).Assembly);

            // OPTIONAL: Register the Autofac filter provider.
            builder.RegisterWebApiFilterProvider(config);

            // OPTIONAL: Register the Autofac model binder provider.
            builder.RegisterWebApiModelBinderProvider();

            builder.Register<ServiceFactory>(ctx =>
            {
                IComponentContext c = ctx.Resolve<IComponentContext>();
                return t => c.Resolve(t);
            });

            builder.RegisterAssemblyTypes(typeof(IMediator).GetTypeInfo().Assembly).AsImplementedInterfaces();

            builder.RegisterType<UserService>()
                   .As<IdentityServer3.Core.Services.IUserService>()
                   .InstancePerDependency();

            IContainer container = builder.Build();
            // Set MVC DI resolver to use our Autofac container
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));

            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);
            return container;
        }
    }
}