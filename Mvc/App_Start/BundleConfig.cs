using System.Web;
using System.Web.Optimization;

namespace ARQ.Maqueta.Presentation.Mvc
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/common").Include(
                        "~/Scripts/ui.utils.js"
            ));

            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-1.*"
            ));

            bundles.Add(new ScriptBundle("~/bundles/datatables").Include(
                        "~/scripts/datatables/jquery.datatables.js"
            ));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.unobtrusive*",
                        "~/Scripts/jquery.validate*"
            ));

            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"
            ));

            bundles.Add(new ScriptBundle("~/bundles/localize-es").Include(
                        "~/Scripts/globalize.js",
                        "~/Scripts/i18n/datatables.locale-es.js",
                        "~/Scripts/locales/bootstrap-datepicker.es.js",
                        "~/Scripts/i18n/ui.locale-es.js",
                        "~/Scripts/i18n/globalize.culture.es-ES.js"
            ));

            bundles.Add(new ScriptBundle("~/bundles/localize-en").Include(
                        "~/Scripts/globalize.js",
                        "~/Scripts/i18n/datatables.locale-en.js",
                        "~/Scripts/i18n/ui.locale-en.js",
                        "~/Scripts/i18n/globalize.culture.en-US.js"
            ));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                        "~/Scripts/bootstrap.js",
                        "~/Scripts/bootstrap-datepicker.js",
                        "~/Scripts/respond.js"
            ));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                        "~/Content/bootstrap.css",
                        "~/Content/bootstrap-theme.css",
                        "~/Content/bootstrap-datepicker3.css",
                        "~/content/datatables/css/jquery.dataTables.css",
                        "~/content/custom-datatables.css",
                        "~/Content/site.css"
            ));



        }
    }
}