using Autofac;
using MediatR;
using MediatR.Pipeline;

namespace DT.MasterData.WebApi
{
    public class MasterDataModule : Module
    {
        public MasterDataModule()
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