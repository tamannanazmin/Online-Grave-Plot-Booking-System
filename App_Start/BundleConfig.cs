using System.Web;
using System.Web.Optimization;

namespace GraveBooking
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/popper").Include(
                       "~/Scripts/popper.js*"));

            bundles.Add(new ScriptBundle("~/bundles/login").Include(
                      "~/Scripts/login.js*"));

            bundles.Add(new ScriptBundle("~/bundles/registration").Include(
                     "~/Scripts/registration.js*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need.

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.min.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/style.css",
                      "~/Content/pop-over.css"
                     ));
            bundles.Add(new StyleBundle("~/Content/css/login").Include(
                     "~/Content/login.css"
                    ));
            bundles.Add(new StyleBundle("~/Content/css/profile").Include(
                    "~/Content/profile.css"
                   ));
            bundles.Add(new StyleBundle("~/Content/css/user-info").Include(
                   "~/Content/user-info.css"
                  ));
        }
    }
}
