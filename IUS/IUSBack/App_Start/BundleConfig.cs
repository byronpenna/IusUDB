using System.Web;
using System.Web.Optimization;

namespace IUSBack
{
    public class BundleConfig
    {
        // For more information on Bundling, visit http://go.microsoft.com/fwlink/?LinkId=254725
        public static void RegisterBundles(BundleCollection bundles)
        {
            // scripts 
                bundles.Add(new ScriptBundle("~/bundles/scriptsGenerales").Include(
                    "~/Scripts/Generales/jquery-{version}.js"
                    ).Include(
                    "~/Content/third-party/bootstrap/js/bootstrap.js"
                    ).Include(
                    "~/Scripts/Generales/script.js",
                    "~/Scripts/Generales/functions.js"
                    ));
            // css
                bundles.Add(new StyleBundle("~/bundles/bootstrap").Include(
                        "~/Content/third-party/bootstrap/css/bootstrap.css"
                    ).Include(
                        "~/Content/themes/iusback_theme/generals/style.css"
                    ).Include(
                        "~/Content/themes/iusback_theme/generals/media.css"
                    )
                );
        }
    }
}