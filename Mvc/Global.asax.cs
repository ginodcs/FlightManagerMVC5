using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using ARQ.Maqueta.Entities;
using ARQ.Maqueta.Presentation.Mvc.Controllers;
using ARQ.Maqueta.Presentation.Mvc.Extensions.Helpers;

namespace ARQ.Maqueta.Presentation.Mvc
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            //Database.SetInitializer(new DbIntializer<DatabaseNotFoundInitializer>());

            AreaRegistration.RegisterAllAreas();

            ViewEngineConfig.SetupViewEngines();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
			BinderConfig.RegisterModelBinders();
			EntityMappingsConfig.CreateMaps();
            BundleTable.EnableOptimizations = false;

        }

        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("gestionErrores.axd");
        }

        protected void Application_AcquireRequestState(object sender, System.EventArgs e)
        {
            // Globalization
            // Change this if you want to add a new culture
            string[] acceptedCultures = new string[] {"en-US" };
            IEnumerable<CultureInfo> cultures = acceptedCultures.Select(c => new CultureInfo(c));

            if (HttpContext.Current.Session != null)
            {
                //RouteData route = HttpContext.Current.Request.RequestContext.RouteData;
                //var lang = HttpContext.Current.Request.QueryString["lang"];

                //if (lang != null && !string.IsNullOrEmpty(lang.ToString()))
                //{
                //    Culture = CultureInfo.CreateSpecificCulture(lang);
                //}
                //else if (Culture == null)
                //{
                //    IEnumerable<CultureInfo> preferredCultures = HttpContext.Current.Request.UserLanguages.Select(c => new CultureInfo(c.Split(';')[0]));

                //    foreach (CultureInfo preferredCulture in preferredCultures)
                //    {
                //        // Find exact culture
                //        Culture = cultures.FirstOrDefault(c => c.Equals(preferredCulture));

                //        // Find two letters culture
                //        if (Culture == null)
                //            Culture = cultures.FirstOrDefault(c => c.TwoLetterISOLanguageName == preferredCulture.TwoLetterISOLanguageName);

                //        // Culture found
                //        if (Culture != null)
                //            break;
                //    }

                //    // Default culture
                //    if (Culture == null)
                //        Culture = cultures.First();
                //}
                Thread.CurrentThread.CurrentUICulture = cultures.First(); ;
                Thread.CurrentThread.CurrentCulture = cultures.First(); ;
            }
        }

        protected void Application_Error(object sender, EventArgs e)
        {
            //Exception ex = Server.GetLastError();
            //if (ex != null)
            //{
            //    StringBuilder err = new StringBuilder();
            //    err.Append("Error caught in Application_Error event\n");
            //    err.Append("Error in: " + (Context.Session == null ? string.Empty : Request.Url.ToString()));
            //    err.Append("\nError Message:" + ex.Message);
            //    if (null != ex.InnerException)
            //        err.Append("\nInner Error Message:" + ex.InnerException.Message);
            //    err.Append("\n\nStack Trace:" + ex.StackTrace);
            //    Server.ClearError();

            //    if (null != Context.Session)
            //    {
            //        err.Append($"Session: Identity name:[{Thread.CurrentPrincipal.Identity.Name}] IsAuthenticated:{Thread.CurrentPrincipal.Identity.IsAuthenticated}");
            //    }

            //    if (null != Context.Session)
            //    {
            //        var routeData = new RouteData();
            //        routeData.Values.Add("controller", "ErrorPage");
            //        routeData.Values.Add("exception", ex);

            //        if (ex.GetType() == typeof(HttpException))
            //        {
            //            int statusCode = ((HttpException)ex).GetHttpCode();

            //            if (statusCode == (int)HttpStatusCode.Unauthorized)
            //            {
            //                routeData.Values.Add("action", "Unauthorized");
            //            }
            //            else
            //            {
            //                routeData.Values.Add("action", "Error");
            //            }

            //            routeData.Values.Add("statusCode", (int)statusCode);

            //        }
            //        else if (ex.GetType() == typeof(ApiException))
            //        {
            //            string statusCode = ex.Source;

            //            if (statusCode == HttpStatusCode.Forbidden.ToString())
            //            {
            //                routeData.Values.Add("action", "Unauthorized");
            //            }
            //            else
            //            {
            //                routeData.Values.Add("action", "Error");
            //            }

            //            routeData.Values.Add("statusCode", statusCode);

            //        }
            //        else
            //        {
            //            routeData.Values.Add("statusCode", 500);
            //            routeData.Values.Add("action", "Error");
            //        }

            //        Response.TrySkipIisCustomErrors = true;

            //        IController controller = new ErrorPageController();
            //        controller.Execute(new RequestContext(new HttpContextWrapper(Context), routeData));
            //        Response.End();
            //    }
            //}
        }

        private CultureInfo Culture
        {
            get
            {
                return HttpContext.Current.Session["Culture"] as CultureInfo;
            }
            set
            {
                HttpContext.Current.Session["Culture"] = value;
            }
        }
    }
}
