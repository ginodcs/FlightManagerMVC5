using System;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.Owin.Security;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using ARQ.Maqueta.Presentation.Mvc.Extensions.Controllers;
using ARQ.Maqueta.Presentation.Mvc.Models.Account;
using System.Web.Security;
using System.Configuration;
using System.Collections;
using ARQ.Maqueta.Presentation.Mvc.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using ARQ.Maqueta.Presentation.Mvc.App_Start;
using System.Security.Principal;
using ARQ.Maqueta.Presentation.Mvc.ApiCall;

namespace ARQ.Maqueta.Presentation.Mvc.Controllers
{
    public class AccountController : BaseController
    {
      
        public AccountController ()
        {

        }

        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        //
        // POST: /Account/Login
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginViewModel model, string returnUrl)
        {
            
            if (ModelState.IsValid)
            {
                // Replace with your own logic:
                var username = model.UserName;
                var fullName = model.UserName;
                var roles = new[] { "Administrador" };
                               
                var user = new ApplicationUser() { UserName = model.UserName };
                    
                AuthenticationManager.SignOut(DefaultAuthenticationTypes.ExternalCookie);

                var identity = this.CreateIdentity(username, fullName, roles);

                AuthenticationManager.SignIn(
                    new AuthenticationProperties()
                    {
                        IsPersistent = model.RememberMe,
                        ExpiresUtc = new DateTimeOffset(DateTime.Now.AddDays(15)).ToUniversalTime()
                    },
                identity);

                await new ApiCall.HttpComposer().InitializeAsync(model.UserName, model.Password);

                return RedirectToAction("Index", "Home");
                
            }
            return View(model);
        }

        //
        // POST: /Account/ExternalLogin
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult ExternalLogin(string provider, string returnUrl)
        {
            // Request a redirect to the external login provider
            return new ChallengeResult(provider, Url.Action("ExternalLoginCallback", "Account", new { ReturnUrl = returnUrl }));
        }

        // GET: /Account/ExternalLoginCallback
        [AllowAnonymous]
        public async Task<ActionResult> ExternalLoginCallback(string returnUrl)
        {
            var loginInfo = await AuthenticationManager.GetExternalLoginInfoAsync();
            if (loginInfo == null)
            {
                return RedirectToAction("Login");
            }

            // Replace with your own logic:
            var username = loginInfo.DefaultUserName;
            var fullName = loginInfo.DefaultUserName;

            var roles = new[]{"Administrador"};

            var identity = CreateIdentity(username, fullName, roles);
            AuthenticationManager.SignIn(new AuthenticationProperties() { IsPersistent = false }, identity);
            return RedirectToLocal(returnUrl);
        }


        // POST: /Account/LogOff
        [HttpPost]
        [AllowAnonymous]
        //[ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            AuthenticationManager.SignOut();

            return RedirectToAction("Index", "Home");
        }

        public ActionResult LogOf()
        {
            AuthenticationManager.SignOut();
            return RedirectToAction("Index", "Home");
        }

        #region Private helpers

        private bool ValidarUsuario(string LoginUsuario, string PasswordUsuario)
        {
           return true;
        }
       

        /// <summary>
        /// Gets the authentication manager from the OWIN context
        /// </summary>
        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        /// <summary>
        /// Redirects the browser to the specified URL, if it is local.
        /// </summary>
        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        /// <summary>
        /// Creates a simple ClaimsIdentity object
        /// </summary>
        private ClaimsIdentity CreateIdentity(string username, string fullName, IEnumerable<string> roles)
        {
            var identity = new ClaimsIdentity(DefaultAuthenticationTypes.ApplicationCookie, ClaimTypes.NameIdentifier, ClaimTypes.Role);
            identity.AddClaim(new Claim("http://schemas.microsoft.com/accesscontrolservice/2010/07/claims/identityprovider", "ASP.NET Identity"));
            identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, username));
            identity.AddClaim(new Claim(ClaimTypes.Name, fullName));
            if (roles != null && roles.Any())
            {
                foreach (var role in roles)
                {
                    identity.AddClaim(new Claim(ClaimTypes.Role, role));
                }
            }
            return identity;
        }

        private class ChallengeResult : HttpUnauthorizedResult
        {
            public ChallengeResult(string provider, string redirectUri)
                : this(provider, redirectUri, null)
            {
            }

            public ChallengeResult(string provider, string redirectUri, string userId)
            {
                LoginProvider = provider;
                RedirectUri = redirectUri;
                UserId = userId;
            }

            public string LoginProvider { get; set; }
            public string RedirectUri { get; set; }
            public string UserId { get; set; }
            private const string XsrfKey = "XsrfId";

            public override void ExecuteResult(ControllerContext context)
            {
                var properties = new AuthenticationProperties() { RedirectUri = RedirectUri };
                if (UserId != null)
                {
                    properties.Dictionary[XsrfKey] = UserId;
                }
                context.HttpContext.GetOwinContext().Authentication.Challenge(properties, LoginProvider);
            }
        }
        #endregion

    }
}
