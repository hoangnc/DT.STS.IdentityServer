using DT.STS.IdentityServer.Application.Users.Commands;
using DT.STS.IdentityServer.Mvc.Models;
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
using System.Linq;
using IdentityServer3.Core;

namespace DT.STS.IdentityServer.Mvc.Controllers
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
            LoginModel loginModel = new LoginModel();
            loginModel.AvailableDomains = GetDomains();
            return View(loginModel);
        }

        [Route("identity/account/login")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(string id, LoginModel model)
        {
            if (ModelState.IsValid)
            {
                IDictionary<string, object> env = Request.GetOwinContext().Environment;

                AuthenticateDto authenticateResult = await _mediator.Send(new AuthenticateCommand
                {
                    Domain = model.Domain,
                    UserName = model.UserName,
                    Password = model.Password
                });

                if (authenticateResult.AccountStatus == Wdc.DirectoryLib.Types.AccountStatus.Success)
                {
                    List<Claim> claims = new List<Claim>();
                    if (authenticateResult.User.JpegPhoto != null)
                        claims.Add(new Claim(Common.Constants.DtClaimTypes.UserImage, Convert.ToBase64String(authenticateResult.User.JpegPhoto)));

                    claims.Add(new Claim(Constants.ClaimTypes.GivenName, $"{authenticateResult.User.DisplayName }"));
                    claims.Add(new Claim(Constants.ClaimTypes.Email, $"{ authenticateResult.User.UserPrincipalName}"));

                    env.IssueLoginCookie(new IdentityServer3.Core.Models.AuthenticatedLogin
                    {
                        AuthenticationMethod = CookieAuthenticationDefaults.AuthenticationType,
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
            model.AvailableDomains = GetDomains();
            return View(model);
        }

        [Route("account/signout")]
        [AllowAnonymous]
        public ActionResult Signout()
        {
            Request.GetOwinContext().Authentication.SignOut();
            return Redirect("/");
        }

        public void SignoutCleanup(string sid)
        {
            ClaimsPrincipal cp = (ClaimsPrincipal)User;
            Claim sidClaim = cp.FindFirst("sid");
            if (sidClaim != null && sidClaim.Value == sid)
            {
                Request.GetOwinContext().Authentication.SignOut(CookieAuthenticationDefaults.AuthenticationType);
            }
        }

        private  IList<SelectListItem> GetDomains()
        {
           return new List<SelectListItem>
            {
                new SelectListItem
                {
                    Text = "Duy Tân",
                    Value = "duytan.local"
                },
                new SelectListItem
                {
                    Text = "Khách",
                    Value="Customer"
                }
            };
        }
    }
}