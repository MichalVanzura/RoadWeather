using System.Web;
using System.Web.Optimization;

namespace RoadWeather
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/angular").Include(
                        "~/Scripts/angular.min.js",
                        "~/Scripts/ngAutocomplete.js",
                        "~/Scripts/angular-route.min.js",
                        "~/Scripts/angular-route.js",
                        "~/Scripts/angular-resource.min.js",
                        "~/Scripts/ui-bootstrap-0.11.2.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/angularGoogleMaps").Include(
                        "~/Scripts/angular-google-maps.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/loadash").Include(
                        "~/Scripts/lodash.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/underscore").Include(
                        "~/Scripts/underscore-min.js"));

            bundles.Add(new ScriptBundle("~/bundles/angularApp").Include(
                        "~/Scripts/app.js",
                        "~/Scripts/controllers.js",
                        "~/Scripts/directives.js",
                        "~/Scripts/services.js"));


            bundles.Add(new ScriptBundle("~/bundles/datetimepicker").Include(
                       "~/Scripts/jquery.datetimepicker.js"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css",
                      "~/Content/small.css",
                      "~/Content/bootstrap-datepicker.css",
                      "~/Content/jquery.timepicker.css"));
        }
    }
}
