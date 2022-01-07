using System.Web;
using System.Web.Optimization;

namespace intraweb_rev3
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add((new ScriptBundle("~/bundles/angular")).Include("~/Scripts/angular/angular.js"));
            bundles.Add((new ScriptBundle("~/bundles/jquery")).Include("~/Scripts/jquery-{version}.js"));
            bundles.Add((new ScriptBundle("~/bundles/jqueryval")).Include("~/Scripts/jquery.validate*"));
            bundles.Add((new ScriptBundle("~/bundles/jqueryui")).Include("~/Scripts/jquery-ui-{version}.js"));
            bundles.Add((new StyleBundle("~/Content/themes/redmond/css")).Include("~/Content/themes/redmond/*.css"));
            bundles.Add((new ScriptBundle("~/bundles/modernizr")).Include("~/Scripts/modernizr-*"));
            bundles.Add((new ScriptBundle("~/bundles/bootstrap")).Include(new string[3]
            {
                "~/Scripts/bootstrap.js",
                "~/Scripts/respond.js",
                "~/Scripts/ui-bootstrap-tpls.min.js"
            }));
            bundles.Add((new StyleBundle("~/Content/css")).Include(new string[4]
            {
                "~/Content/bootstrap.css",
                "~/Content/site.css",
                "~/Content/Spinner.css",
                "~/Content/Utils.css"
            }));
            bundles.Add((new ScriptBundle("~/bundles/ckeditor")).Include(new string[2]
            {
                "~/Scripts/ckeditor/ckeditor.js",
                "~/Scripts/angular/angular-ckeditor.js"
            }));
            bundles.Add((new ScriptBundle("~/bundles/ng-grid")).Include(new string[2]
            {
                "~/Scripts/angular/ng-grid.css",
                "~/Scripts/angular/ng-grid.min.js"
            }));
            bundles.Add((new ScriptBundle("~/bundles/app")).Include("~/ScriptApp/App/*.js"));
            bundles.Add((new ScriptBundle("~/bundles/controllerDistribution")).Include("~/ScriptApp/ControllerDistribution/*.js"));
            bundles.Add((new ScriptBundle("~/bundles/controllerEcommerce")).Include("~/ScriptApp/ControllerEcommerce/*.js"));
            bundles.Add((new ScriptBundle("~/bundles/controllerRnD")).Include("~/ScriptApp/ControllerRnD/*.js"));
            bundles.Add((new ScriptBundle("~/bundles/controllerBOD")).Include("~/ScriptApp/ControllerBOD/*.js"));
            bundles.Add((new ScriptBundle("~/bundles/controllerLegal")).Include("~/ScriptApp/ControllerLegal/*.js"));
            bundles.Add((new ScriptBundle("~/bundles/controllerConnect")).Include("~/ScriptApp/ControllerConnect/*.js"));
        }
    }
}
