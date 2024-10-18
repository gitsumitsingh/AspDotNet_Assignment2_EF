using System.Web;
using System.Web.Optimization;

namespace Assignment2_EF
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css"));

            bundles.Add(new ScriptBundle("~/bundles/jquerybootstrap").Include(
                   "~/Scripts/jquery-{3.3.1}.js", // jQuery from local (you can replace it if you prefer the CDN version)
                   "~/Scripts/popper.min.js",       // Popper.js (should be downloaded in Scripts folder)
                   "~/Scripts/bootstrap.min.js"));  // Bootstrap JS (should be downloaded in Scripts folder)

            // CSS bundle for Bootstrap
            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.min.css"));  //

            // DataTables CSS bundle
            bundles.Add(new StyleBundle("~/bundles/datatables-css").Include(
                        "~/Content/jquery.dataTables.min.css"));

            // DataTables JS bundle
            bundles.Add(new ScriptBundle("~/bundles/datatables-js").Include(
                        "~/Scripts/jquery-3.7.0.min.js",
                        "~/Scripts/jquery.dataTables.min.js"));

            BundleTable.EnableOptimizations = true;
        }
    }
}
