using Autofac;
using Autofac.Integration.WebApi;
using DT.STS.IdentityServer.Application;
using DT.STS.IdentityServer.Application.Mapper;
using DT.STS.IdentityServer.Persistence;
using MediatR;
using Newtonsoft.Json.Serialization;
using System.Configuration;
using System.Reflection;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace DT.MasterData.WebApi
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
            builder.RegisterModule(new MasterDataModule());

            // Get your HttpConfiguration.
            HttpConfiguration config = GlobalConfiguration.Configuration;


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

            builder.RegisterAssemblyTypes(typeof(IMediator)
                .GetTypeInfo()
                .Assembly)
                .AsImplementedInterfaces();

            IContainer container = builder.Build();

            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);

            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            GlobalConfiguration.Configuration.Formatters.XmlFormatter.SupportedMediaTypes.Clear();
#if DEBUG
            // Pretty json for developers.
            GlobalConfiguration.Configuration.Formatters.JsonFormatter.SerializerSettings.Formatting = Newtonsoft.Json.Formatting.Indented;
#else
            GlobalConfiguration.Configuration.Formatters.JsonFormatter.SerializerSettings.Formatting = Newtonsoft.Json.Formatting.None;
#endif
            GlobalConfiguration.Configuration.Formatters.JsonFormatter.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();

            return container;
        }
    }
}