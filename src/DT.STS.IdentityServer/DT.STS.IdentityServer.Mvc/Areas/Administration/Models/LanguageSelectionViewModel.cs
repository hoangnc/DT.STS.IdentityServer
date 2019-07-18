using Abp.Localization;
using System.Collections.Generic;

namespace DT.STS.IdentityServer.Mvc.Administration.Models
{
    public class LanguageSelectionViewModel
    {
        public LanguageInfo CurrentLanguage { get; set; }

        public IReadOnlyList<LanguageInfo> Languages { get; set; }

        public string CurrentUrl { get; set; }
    }
}