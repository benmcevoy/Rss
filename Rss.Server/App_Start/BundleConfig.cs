using System.Web;
using System.Web.Optimization;

namespace Rss.Server
{
    public class BundleConfig
    {
        // For more information on Bundling, visit http://go.microsoft.com/fwlink/?LinkId=254725
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js",
                        "~/Scripts/jQuery.tmpl.js"));


            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/knockout")
                .Include("~/Scripts/knockout-2.2.1.js"));


            bundles.Add(new ScriptBundle("~/bundles/rss")
                            .Include("~/Scripts/rss/namespace.js", "~/Scripts/rss/communication.js")
                            .IncludeDirectory("~/Scripts/rss/Models", "*.js")
                            .IncludeDirectory("~/Scripts/rss/ViewModels", "*ViewModel.js")
                            // site must come last
                            .Include("~/Scripts/rss/site.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include("~/Content/site.css"));

        }
    }
}