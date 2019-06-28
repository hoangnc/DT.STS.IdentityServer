using Autofac;
using DT.Core.Web.Ui.Navigation;
using DT.STS.IdentityServer.Mvc.Areas.Administration.Services;
using MediatR;
using MediatR.Pipeline;

namespace DT.STS.IdentityServer.Mvc
{
    public class STSIdentityServerModule : Module
    {
        public STSIdentityServerModule()
        {

        }

        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterGeneric(typeof(RequestPreProcessorBehavior<,>))
                .As(typeof(IPipelineBehavior<,>));

            builder.RegisterGeneric(typeof(RequestPostProcessorBehavior<,>))
               .As(typeof(IPipelineBehavior<,>));

            builder.RegisterType<MenuConfigurationContext>()
                .As<IMenuConfigurationContext>()
                .InstancePerRequest();

            builder.RegisterType<MenuManager>()
                .As<IMenuManager>()
                .InstancePerRequest();

            base.Load(builder);
        }
    }
}