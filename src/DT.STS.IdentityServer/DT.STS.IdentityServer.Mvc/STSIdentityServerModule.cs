using Abp.Localization;
using Abp.Localization.Dictionaries;
using Abp.Localization.Dictionaries.Json;
using Abp.Localization.Sources;
using Abp.Web.Configuration;
using Abp.Web.Localization;
using Autofac;
using Autofac.Integration.Mvc;
using DT.Core.Authorization;
using DT.Core.Localization;
using DT.Core.Web.Common;
using DT.Core.Web.Common.Mvc.Controllers;
using DT.Core.Web.Localization;
using DT.Core.Web.Ui.Navigation;
using DT.STS.IdentityServer.Mvc.Areas.Administration.Mapper;
using DT.STS.IdentityServer.Mvc.Areas.Administration.Services;
using MediatR;
using MediatR.Pipeline;
using System.Web;

namespace DT.STS.IdentityServer.Mvc
{
    public class STSIdentityServerModule : Module
    {
        public STSIdentityServerModule()
        {

        }

        protected override void Load(ContainerBuilder builder)
        {
            MvcAutoMapperConfiguration.Initialize();

            builder.RegisterGeneric(typeof(RequestPreProcessorBehavior<,>))
                .As(typeof(IPipelineBehavior<,>));

            builder.RegisterGeneric(typeof(RequestPostProcessorBehavior<,>))
                .As(typeof(IPipelineBehavior<,>));

            builder.RegisterType<MenuConfigurationContext>()
                .As<IMenuConfigurationContext>()
                .InstancePerRequest();

            builder.RegisterType<PermissionChecker>()
                .As<IPermissionChecker>()
                .InstancePerRequest();

            ILocalizationConfiguration localization = new LocalizationConfiguration();
            localization.Languages.Add(new LanguageInfo("en", "English", icon: "famfamfam-flag-england", isDefault: false));
            localization.Languages.Add(new LanguageInfo("vi-VN", "Tiếng Việt", icon: "famfamfam-flag-vn", isDefault: true));
            DictionaryBasedLocalizationSource localizationSource = new DictionaryBasedLocalizationSource(
                "IdentityServer",
                new JsonFileLocalizationDictionaryProvider(
                    HttpContext.Current.Server.MapPath("~/Localization/JsonSources")));

            localization.Sources.Add(localizationSource);

            builder.Register(c => localizationSource)
               .As<ILocalizationSource>()
               .SingleInstance();

            builder.Register(c => localization)
                .As<ILocalizationConfiguration>()
                .SingleInstance();

            builder.Register(c => new AbpWebLocalizationConfiguration())
                .As<IAbpWebLocalizationConfiguration>()
                .SingleInstance();

            builder.Register(c => new CurrentCultureSetter())
                .As<ICurrentCultureSetter>()
                .SingleInstance();

            builder.RegisterType<DefaultLanguageProvider>()
                .As<ILanguageProvider>()
                .SingleInstance();

            builder.RegisterType<LanguageManager>()
                .As<ILanguageManager>()
                .SingleInstance();

            builder.RegisterModule(new DTWebCommonModule(localization));

            builder.Register(c => new LocalizationManager(c.Resolve<ILanguageManager>(),
                                         c.Resolve<ILocalizationConfiguration>(), null))
                .As<ILocalizationManager>()
                .OnActivated(e => e.Instance.Initialize())
                .SingleInstance();

            builder.RegisterType<MenuManager>()
                .As<IMenuManager>()
                .InstancePerRequest();

            builder.RegisterControllers(typeof(LocalizationController).Assembly);

            base.Load(builder);
        }
    }
}