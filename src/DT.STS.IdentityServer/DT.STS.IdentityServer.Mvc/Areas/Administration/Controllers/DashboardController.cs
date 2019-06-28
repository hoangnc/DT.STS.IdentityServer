using DT.Core.Web.Ui.Navigation;
using DT.STS.IdentityServer.Mvc.Areas.Administration.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DT.STS.IdentityServer.Mvc.Areas.Administration.Controllers
{
    public class DashboardController : Controller
    {
        // GET: Administration/Dashboard
        [Authorize]
        [Menu(SelectedMenu = MenuNameConstants.User)]
        public ActionResult Index()
        {
            return View();
        }
    }
}