using System.Web.Optimization;

namespace WebUI
{
    public class BundleConfig
    {
        // For more information on Bundling, visit http://go.microsoft.com/fwlink/?LinkId=254725
        public static void RegisterBundles(BundleCollection bundles)
        {
          /*  bundles.Add(new StyleBundle("~/Content/bootstrawesome").Include(
                "~/Content/Bootstrawesome.css"));


            bundles.Add(new StyleBundle("~/Content/other").Include(
                "~/Content/themes/jquery-ui-1.10.4.custom.min.css",
                "~/Content/LightBox/screen.css",
                "~/Content/LightBox/lightbox.css",
                "~/Content/Site2.css"));


            bundles.Add(new ScriptBundle("~/bundles/scripts").Include(
                "~/Scripts/jquery-1.7.1.min.js",
                "~/Scripts/js/jquery-ui-1.9.2.custom.min.js",
                "~/Scripts/js/bootstrap.min.js",
                "~/Scripts/jquery.unobtrusive-ajax.min.js",
                "~/Scripts/jquery.validate.min.js",
                "~/Scripts/jquery.validate.unobtrusive.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/common").Include(
                "~/Scripts/js/commonTmp.js",
                "~/Scripts/js/grid-listTMP.js"));
            */

            //---Admin part
            /*bundles.Add(new StyleBundle("~/Content/admin_bootstrawesome").Include(
               "~/Content/bootstrap.min.css",
               "~/Content/font-awesome.min.css"));



            bundles.Add(new StyleBundle("~/Content/admin_css").Include(
               "~/Content/Admin2.css",
               "~/Content/jquery_ui_datepicker.css"));

          
            bundles.Add(new ScriptBundle("~/bundles/admin_bootstrawesome-js").Include(
              "~/Scripts/js/bootstrap.min.js"));

            
            bundles.Add(new ScriptBundle("~/bundles/admin_scripts").Include(
                "~/Scripts/jquery-1.8.3.min.js",
                "~/Scripts/jquery-ui-1.9.2.custom.min.js",
                "~/Scripts/jquery.validate.min.js",
                "~/Scripts/jquery.validate.unobtrusive.min.js",
                "~/Scripts/jquery-FixingBug.js",
                "~/Scripts/jquery.unobtrusive-ajax.min.js"));


            bundles.Add(new ScriptBundle("~/bundles/admin_masked-datepiker").Include(
               
                "~/Scripts/js/jquery.maskedinput.min.js",
                "~/Scripts/js/datepicker.js"));*/
        }
        
    }
}

/*
       <script src="@Url.Content("~/Scripts/jquery-1.8.3.min.js")" type="text/javascript"></script>
        <script src="@Url.Content("~/Scripts/js/jquery-ui-1.9.2.custom.min.js")" type="text/javascript"></script>
        
        <script src="@Url.Content("~/Scripts/js/bootstrap.min.js")" type="text/javascript"></script>
        
        <script src="@Url.Content("~/Scripts/jquery.unobtrusive-ajax.min.js")" type="text/javascript"></script>
        <script src="@Url.Content("~/Scripts/jquery.validate.min.js")" type="text/javascript"></script>
        <script src="@Url.Content("~/Scripts/jquery.validate.unobtrusive.min.js")" type="text/javascript"></script>
 
    bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));
 
            bundles.Add(new ScriptBundle("~/bundles/jqueryui").Include(
                        "~/Scripts/jquery-ui-{version}.js"));
 
            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.unobtrusive*",
                        "~/Scripts/jquery.validate*"));
 
 */