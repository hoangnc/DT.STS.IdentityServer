﻿using Abp.Localization;
using DT.STS.IdentityServer.Mvc.Administration.Models;
using MediatR;
using System.Linq;
using System.Web.Mvc;

namespace DT.STS.IdentityServer.Mvc.Areas.Administration.Controllers
{
    public class IdentityServerControllerBase : Controller
    {
        private readonly ILanguageManager _languageManager = DependencyResolver.Current.GetService<ILanguageManager>();
        private IMediator _mediator;
        protected IMediator Mediator => _mediator ?? (_mediator = DependencyResolver.Current.GetService(typeof(IMediator)) as IMediator);

        [ChildActionOnly]
        public PartialViewResult _LanguagesPartial()
        {
            LanguageSelectionViewModel model = new LanguageSelectionViewModel
            {
                CurrentLanguage = _languageManager.CurrentLanguage,
                Languages = _languageManager.GetLanguages().Where(l => !l.IsDisabled).ToList()
                    .Where(l => !l.IsDisabled)
                    .ToList(),
                CurrentUrl = Request.Path
            };

            return PartialView("_LanguagesPartial", model);
        }
    }
}