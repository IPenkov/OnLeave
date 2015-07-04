using System.Web;
using System.Web.Optimization;

namespace OnLeave
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*",
                        "~/Scripts/jquery.unobtrusive-ajax*",
                        "~/Scripts/on.leave.js",
                        "~/Scripts/jquery.form*",
                        "~/Scripts/tabs.js",
                        "~/Scripts/cloud-zoom.1.0.2.js",
                        "~/Scripts/jquery-migrate*",
                        "~/Scripts/jquery.jcarousel.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/jquery-ui").Include(
                "~/Scripts/jquery-ui-1.11.1.js"));
                        

            bundles.Add(new ScriptBundle("~/bundles/autosize").Include(
                "~/Scripts/jquery.autosize*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(                      
                      "~/Content/site.css",
                      "~/Content/slideshow.css",
                      "~/Content/cloud-zoom.css",
                      "~/Content/themes/base/*.css"));
        }
    }
}
