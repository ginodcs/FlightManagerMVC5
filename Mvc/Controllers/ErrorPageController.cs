using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ARQ.Maqueta.Presentation.Mvc.Extensions.Controllers;
using ARQ.Maqueta.Presentation.Mvc.ViewModels;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;

namespace ARQ.Maqueta.Presentation.Mvc.Controllers
{
    public class ErrorPageController : BaseController
    {
        public ActionResult Error()
        {
                return View();  
        }

        public ActionResult Unauthorized()
        {
            AuthenticationManager.SignOut();

            return View("~/Views/Shared/Unauthorized.cshtml");
        }

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

    }
}