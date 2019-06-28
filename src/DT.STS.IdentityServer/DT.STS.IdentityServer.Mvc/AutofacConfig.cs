using Autofac;
using Autofac.Integration.Mvc;
using Autofac.Integration.WebApi;
using DT.Core.Web.Ui.Navigation;
using DT.STS.IdentityServer.Application;
using DT.STS.IdentityServer.Mvc.Services;
using DT.STS.IdentityServer.Persistence;
using IdentityServer3.Core.Services;
using MediatR;
using Newtonsoft.Json.Serialization;
using System.Configuration;
using System.Reflection;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace DT.STS.IdentityServer.Mvc
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
            builder.RegisterControllers(typeof(AutofacConfig).Assembly);

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
                   .As<IUserService>()
                   .InstancePerDependency();

            builder.RegisterType<ScopeStore>()
                .As<IScopeStore>()
                .SingleInstance();

            builder.RegisterType<ClientStore>()
                .As<IClientStore>()
                .SingleInstance();

            IContainer container = builder.Build();
            // Set MVC DI resolver to use our Autofac container
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));

            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);

            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            GlobalConfiguration.Configuration.Formatters.XmlFormatter.SupportedMediaTypes.Clear();
            GlobalConfiguration.Configuration.Formatters.JsonFormatter.SerializerSettings.Formatting = Newtonsoft.Json.Formatting.Indented;
            GlobalConfiguration.Configuration.Formatters.JsonFormatter.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();

            return container;
        }
    }
}