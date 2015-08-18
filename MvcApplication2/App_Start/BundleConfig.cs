using System.Web;
using System.Web.Optimization;

namespace MvcApplication2
{
    public class BundleConfig
    {
        // For more information on Bundling, visit http://go.microsoft.com/fwlink/?LinkId=254725
        public static void RegisterBundles(BundleCollection bundles)
        {

            bundles.Add(new ScriptBundle("~/bundles/jquery")
                            .Include(
                                    "~/Content/jquery-ui-1.11.4.custom/external/jquery/jquery.js",
                                    "~/Content/jquery-ui-1.11.4.custom/jquery-ui.js"
                                )
                            );
            bundles.Add(new ScriptBundle("~/bundles/index2")
                            .Include(
                                    "~/Scripts/pages/index2/index2.js",
                                    "~/Scripts/common/common.js"
                                )
                            );

            bundles.Add(new ScriptBundle("~/bundles/angular").Include(
                                    "~/Scripts/common/common.js",
                                    "~/Scripts/angular/angular.js",
                                    "~/Scripts/angular/angular-dragdrop.js",
                                    "~/Scripts/angular-ui/ui-bootstrap-tpls.js",
                                    "~/Scripts/angular-route/angular-route.js",
                                    "~/Scripts/pages/index3/functions.js",
                                    "~/Scripts/pages/index3/controllers.js",
                                    "~/Scripts/pages/index3/directives.js",
                                    "~/Scripts/pages/index3/index3.js"
                                ));

//            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
//                        "~/Scripts/jquery-{version}.js"));
//
//            bundles.Add(new ScriptBundle("~/bundles/jqueryui").Include(
//                        "~/Scripts/jquery-ui-{version}.js"));
//
//            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
//                        "~/Scripts/jquery.unobtrusive*",
//                        "~/Scripts/jquery.validate*"));
//
//            // Use the development version of Modernizr to develop with and learn from. Then, when you're
//            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
//            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
//                        "~/Scripts/modernizr-*"));
//
//            bundles.Add(new StyleBundle("~/Content/css").Include("~/Content/site.css"));
//
//            bundles.Add(new StyleBundle("~/Content/themes/base/css").Include(
//                        "~/Content/themes/base/jquery.ui.core.css",
//                        "~/Content/themes/base/jquery.ui.resizable.css",
//                        "~/Content/themes/base/jquery.ui.selectable.css",
//                        "~/Content/themes/base/jquery.ui.accordion.css",
//                        "~/Content/themes/base/jquery.ui.autocomplete.css",
//                        "~/Content/themes/base/jquery.ui.button.css",
//                        "~/Content/themes/base/jquery.ui.dialog.css",
//                        "~/Content/themes/base/jquery.ui.slider.css",
//                        "~/Content/themes/base/jquery.ui.tabs.css",
//                        "~/Content/themes/base/jquery.ui.datepicker.css",
//                        "~/Content/themes/base/jquery.ui.progressbar.css",
//                        "~/Content/themes/base/jquery.ui.theme.css"));
        }
    }
}