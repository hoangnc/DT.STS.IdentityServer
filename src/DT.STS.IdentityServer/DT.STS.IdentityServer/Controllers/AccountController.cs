using DT.STS.IdentityServer.Application.Users.Commands;
using DT.STS.IdentityServer.Models;
using IdentityServer3.Core.Extensions;
using MediatR;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.OpenIdConnect;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace DT.STS.IdentityServer.Controllers
{
    public class AccountController : Controller
    {
        private readonly IMediator _mediator;

        public AccountController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [Route("identity/account/login")]
        public ActionResult Login(string id)
        {
            return View();
        }

        [Route("identity/account/login")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(string id, SigInModel model)
        {
            if (ModelState.IsValid)
            {
                IDictionary<string, object> env = Request.GetOwinContext().Environment;

                AuthenticateDto authenticateResult = await _mediator.Send(new AuthenticateCommand
                {
                    UserName = model.UserName,
                    Password = model.Password
                });

                if (authenticateResult.AccountStatus == Wdc.DirectoryLib.Types.AccountStatus.Success)
                {
                    List<Claim> claims = new List<Claim>();
                    if (authenticateResult.User.JpegPhoto != null)
                        new Claim(Common.Constants.DtClaimTypes.UserImage, Convert.ToBase64String(authenticateResult.User.JpegPhoto));

                    env.IssueLoginCookie(new IdentityServer3.Core.Models.AuthenticatedLogin
                    {
                        AuthenticationMethod = "Cookies",
                        Subject = authenticateResult.User.DisplayName,
                        Name = authenticateResult.User.SamAccountName,
                        Claims = claims,
                        PersistentLogin = true
                    });

                    ClaimsPrincipal user = (ClaimsPrincipal)User;
                    IdentityServer3.Core.Models.SignInMessage msg = env.GetSignInMessage(id);
                    string returnUrl = msg.ReturnUrl;

                    env.RemovePartialLoginCookie();

                    return Redirect(returnUrl);
                }
                else
                {
                    ModelState.AddModelError("", authenticateResult.Message);
                }
            }

            return View(model);
        }

        [Route("account/signout")]
        [AllowAnonymous]
        public ActionResult Signout()
        {
            Request.GetOwinContext().Authentication.SignOut(new AuthenticationProperties { RedirectUri = "/" },
            OpenIdConnectAuthenticationDefaults.AuthenticationType,
            CookieAuthenticationDefaults.AuthenticationType);
            return Redirect("/");
        }

        public void SignoutCleanup(string sid)
        {
            ClaimsPrincipal cp = (ClaimsPrincipal)User;
            Claim sidClaim = cp.FindFirst("sid");
            if (sidClaim != null && sidClaim.Value == sid)
            {
                Request.GetOwinContext().Authentication.SignOut("Cookies");
            }
        }
    }
}