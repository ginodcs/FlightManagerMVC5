using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ARQ.Maqueta.Presentation.Mvc.Extensions.Controllers;

namespace ARQ.Maqueta.Presentation.Mvc.Controllers
{
    public class HomeController : BaseController
    {
        public ActionResult Index()
        {
                return View();            
        }

        public ActionResult Error()
        {
            return View("~/Views/Shared/Error.cshtml");
        }

        public ActionResult About()
        {
            return View();
        }
    }
}
