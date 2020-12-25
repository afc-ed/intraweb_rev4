using System.Web;
using System.Web.Optimization;

namespace intraweb_rev3
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(((Bundle)new ScriptBundle("~/bundles/angular")).Include(new string[2]
            {
                "~/Scripts/angular.js",
                "~/Scripts/angular-ui-router.js"
            }));
            bundles.Add(((Bundle)new ScriptBundle("~/bundles/jquery")).Include("~/Scripts/jquery-{version}.js", new IItemTransform[0]));
            bundles.Add(((Bundle)new ScriptBundle("~/bundles/jqueryval")).Include("~/Scripts/jquery.validate*", new IItemTransform[0]));
            bundles.Add(((Bundle)new ScriptBundle("~/bundles/jqueryui")).Include("~/Scripts/jquery-ui-{version}.js", new IItemTransform[0]));
            bundles.Add(((Bundle)new StyleBundle("~/Content/themes/redmond/css")).Include("~/Content/themes/redmond/*.css", new IItemTransform[0]));
            bundles.Add(((Bundle)new ScriptBundle("~/bundles/modernizr")).Include("~/Scripts/modernizr-*", new IItemTransform[0]));
            bundles.Add(((Bundle)new ScriptBundle("~/bundles/bootstrap")).Include(new string[3]
            {
                "~/Scripts/bootstrap.js",
                "~/Scripts/respond.js",
                "~/Scripts/ui-bootstrap-tpls.min.js"
            }));
            bundles.Add(((Bundle)new StyleBundle("~/Content/css")).Include(new string[4]
            {
                "~/Content/bootstrap.css",
                "~/Content/site.css",
                "~/Content/Spinner.css",
                "~/Content/Utils.css"
            }));
            bundles.Add(((Bundle)new ScriptBundle("~/bundles/app")).Include("~/ScriptApp/App/*.js", new IItemTransform[0]));
            bundles.Add(((Bundle)new ScriptBundle("~/bundles/controllerDistribution")).Include("~/ScriptApp/ControllerDistribution/*.js", new IItemTransform[0]));
            bundles.Add(((Bundle)new ScriptBundle("~/bundles/controllerEcommerce")).Include("~/ScriptApp/ControllerEcommerce/*.js", new IItemTransform[0]));
            bundles.Add(((Bundle)new ScriptBundle("~/bundles/controllerRnD")).Include("~/ScriptApp/ControllerRnD/*.js", new IItemTransform[0]));
            bundles.Add(((Bundle)new ScriptBundle("~/bundles/controllerBOD")).Include("~/ScriptApp/ControllerBOD/*.js", new IItemTransform[0]));
        }
    }
}
