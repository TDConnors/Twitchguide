using System.Web;
using System.Web.Optimization;

namespace TwitchGuide
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        //"~/Scripts/jquery-{version}.js"
                        ));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      //"~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/Theme/assets/css/bootstrap.css",
                      "~/Content/Theme/assets/css/style.css",
                      "~/Content/Theme/assets/css/style-responsive.css",
                      "~/Content/Theme/assets/font-awesome/css/font-awesome.css",
                      "~/Content/Theme/assets/lineicons/style.css",
                      "~/Content/site.css"
                      ));

            bundles.Add(new ScriptBundle("~/bundles/themeScript").Include(
        "~/Content/Theme/assets/js/jquery.js",
        "~/Content/Theme/assets/js/jquery-1.8.3.min.js",
        "~/Content/Theme/assets/js/bootstrap.min.js",
        "~/Content/Theme/assets/js/jquery.dcjqaccordion.2.7.js",
        "~/Content/Theme/assets/js/jquery.scrollTo.min.js",
        "~/Content/Theme/assets/js/jquery.nicescroll.js",
        "~/Content/Theme/assets/js/jquery.sparkline.js",
        "~/Content/Theme/assets/js/common-scripts.js",
        "~/Content/Theme/assets/js/gritter/js/jquery.gritter.js",
        "~/Content/Theme/assets/js/gritter-conf.js"
        ));
            
        }
    }
}
