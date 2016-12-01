using System.Web;
using System.Web.Optimization;

namespace Vidly
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/lib").Include(
                        "~/Scripts/jquery-{version}.js", //jquery scripts
                        "~/Scripts/bootstrap.js", //bootstrap scripts
                        "~/Scripts/bootbox.js",
                        "~/Scripts/respond.js",
                        "~/Scripts/datatables/jquery.datatables.js", //datatable main required scripts
                        "~/Scripts/datatables/datatables.bootstrap.js",
                        "~/Scripts/typeahead.bundle.js",
                        "~/Scripts/toastr.js"
                ));


            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            //bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
            //            "~/Scripts/jquery-{version}.js"));
            //bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
            //          ));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap-lumen.css",
                      "~/Content/datatables/css/datatables.bootstrap.css",
                      "~/Content/typeahead.css",
                      "~/Content/toastr.css",
                      "~/Content/site.css",
                      "~/Content/bootstrap-theme.css",
                      "~/Content/bootstrap-united.css"));

            // Set EnableOptimizations to false for debugging. For more information,
            // visit http://go.microsoft.com/fwlink/?LinkId=301862
            BundleTable.EnableOptimizations = true;
        }
    }
}
