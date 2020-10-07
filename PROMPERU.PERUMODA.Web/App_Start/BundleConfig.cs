using System.Web.Optimization;

namespace PROMPERU.PERUMODA.Web
{
    public static class BundleConfig
    {
        // Para obtener más información sobre las uniones, visite https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                "~/Scripts/jquery-3.5.1.min.js",
                "~/Scripts/jquery-migrate-3.3.0.min.js",
                "~/Scripts/jquery-ui.min.js"));
            
            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                "~/Scripts/bootstrap.min.js"));
            
            bundles.Add(new ScriptBundle("~/bundles/js").Include(
                "~/Scripts/materialize.min.js",
                "~/Scripts/Slick/slick.min.js",
                "~/Scripts/main.js",
                "~/Scripts/servicios.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                "~/Content/normalize.css",
                "~/Content/bootstrap.min.css",
                "~/Content/materialize.min.css",
                "~/Content/Slick/slick.css",
                "~/Content/Slick/slick-theme.css",
                "~/Content/global.css",
                "~/Content/forms.css",
                "~/Content/home.css",
                "~/Content/agenda.css"));
        }
    }
}