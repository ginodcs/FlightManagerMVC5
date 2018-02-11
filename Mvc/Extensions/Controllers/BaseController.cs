using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using ARQ.Maqueta.Presentation.Mvc.App_Start;
using Microsoft.AspNet.Identity.Owin;

namespace ARQ.Maqueta.Presentation.Mvc.Extensions.Controllers
{
    public abstract class BaseController : Controller
    {
        public string NameControler(MethodBase method)
        {
           
            return string.Format("{0}.{1}", method.ReflectedType.FullName, method.Name);
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
        }
    }
}
