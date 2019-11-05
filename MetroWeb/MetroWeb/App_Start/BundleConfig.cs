using System.Web;
using System.Web.Optimization;

namespace MetroWeb
{
    public class BundleConfig
    {
        // Para obtener más información sobre las uniones, visite https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-3.4.1.min.js"));

            //bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
            //            "~/Scripts/jquery.validate*"));

            // Utilice la versión de desarrollo de Modernizr para desarrollar y obtener información. De este modo, estará
            // para la producción, use la herramienta de compilación disponible en https://modernizr.com para seleccionar solo las pruebas que necesite.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include("~/Scripts/tooltip.js", "~/Scripts/i18n/es.js", "~/Scripts/popper.js",
                      "~/Scripts/bootstrap.js", "~/Scripts/moment-with-locales.min.js"));
            bundles.Add(new ScriptBundle("~/Scripts/js").Include( "~/Scripts/tilt.jquery.min.js", "~/Scripts/select2.min.js", "~/Scripts/SiteScripts.js",
                "~/Scripts/sweetalert2.min.js", "~/Scripts/jquery.mCustomScrollbar.concat.min.js", "~/Scripts/Chart.js", "~/Scripts/bootstrap-datepicker.min.js", "~/Scripts/locales/bootstrap-datepicker.es.min.js"));
            bundles.Add(new ScriptBundle("~/Scripts/main").Include("~/Scripts/main.js"));


            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css", "~/Content/bootstrap.min.css", "~/Content/font-awesome.min.css",
                      "~/Content/site.css", "~/Content/select2.min.css", "~/Content/animate.css",
                      "~/Content/hamburgers.min.css", "~/Content/util.css", "~/Content/main.css",
                      "~/Content/css/font-awesome.min.css", "~/Content/sweetalert2.min.css", "~/Content/jquery.mCustomScrollbar.min.css", "~/Content/layout_styles.css", "~/Content/bootstrap-datepicker.min.css"));


        }
    }
}
