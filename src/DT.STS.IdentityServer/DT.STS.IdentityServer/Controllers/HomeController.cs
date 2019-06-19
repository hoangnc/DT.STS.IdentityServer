using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Thinktecture.IdentityModel.Mvc;

namespace DT.STS.IdentityServer.Controllers
{

    public class HomeController : Controller
    {
        [ResourceAuthorize("Write", "ContactDetails")]
        [HandleForbidden]
        public ActionResult Index()
        {
            var user = User;
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}