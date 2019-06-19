using Autofac;
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

            base.Load(builder);
        }
    }
}