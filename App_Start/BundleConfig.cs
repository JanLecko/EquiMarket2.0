using System.Web;
using System.Web.Optimization;

namespace EquiMarket
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/js").Include(
                "~/Scripts/Script.js"
                ,"~/Scripts/googleMaps.js"
                //, "~/Scripts/hereMaps.js"
                ));

            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js",
                        "~/Scripts/jquery-ui-{version}.js",
                        "~/Scripts/jquery.unobtrusive-ajax.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            //BlueImp js
            bundles.Add(new ScriptBundle("~/bundles/BlueImp").Include(
                                   "~/Scripts/bootstrap-image-gallery.min.js"));
            

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));



            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/yeti.bootstrap.min.css",
                      "~/Content/themes/base/jquery-ui.css",
                      "~/Content/CustomBootstrap.css",
                      "~/Content/themes/base/jquery.ui.autocomplete.css",
                      "~/Content/site.css"));

            //BlueImp css 
            bundles.Add(new StyleBundle("~/Content/BlueImp").Include(
                    "~/content/BlueImp/css/bootstrap-image-gallery.min.css"
                ));

                

            
        }
    }
}
