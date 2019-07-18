using Autofac;
using DT.STS.IdentityServer.Application.Mapper;
using MediatR;
using System;
using System.Reflection;

namespace DT.STS.IdentityServer.Application
{
    public class STSIdentityServerApplicationModule : Autofac.Module
    {
        public STSIdentityServerApplicationModule()
        {

        }

        protected override void Load(ContainerBuilder builder)
        {
            AutoMapperConfiguration.Initialize();

            Type[] mediatrOpenTypes = new[]
            {
                typeof(IRequestHandler<,>),
                typeof(INotificationHandler<>),
            };

            foreach (Type mediatrOpenType in mediatrOpenTypes)
            {
                builder
                    .RegisterAssemblyTypes(typeof(STSIdentityServerApplicationModule).GetTypeInfo().Assembly)
                    .AsClosedTypesOf(mediatrOpenType)
                    .AsImplementedInterfaces();
            }

            base.Load(builder);
        }
    }
}
