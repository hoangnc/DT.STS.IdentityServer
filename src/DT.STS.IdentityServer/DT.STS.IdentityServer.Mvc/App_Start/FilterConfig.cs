using DT.Core.Web.Ui.Navigation;
using System.Web.Mvc;

namespace DT.STS.IdentityServer.Mvc
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            // filters.Add(new MenuActionFilterAttribute());
        }
    }
}
