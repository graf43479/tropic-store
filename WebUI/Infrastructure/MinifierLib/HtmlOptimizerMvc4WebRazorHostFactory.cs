using System.Configuration;

namespace WebUI.Infrastructure.MinifierLib
{
    using System.Web.Mvc;
    using System.Web.WebPages.Razor;
    using HtmlOptimizerMvc4;

    public class HtmlOptimizerMvc4WebRazorHostFactory : MvcWebRazorHostFactory 
    {
        public override WebPageRazorHost CreateHost(string virtualPath, string physicalPath)
        {
            WebPageRazorHost host = base.CreateHost(virtualPath, physicalPath);

            if (host.IsSpecialPage || host.DesignTimeMode)
                return host;
          
            return new HtmlOptimizerMvc4WebPageRazorHost(virtualPath, physicalPath);
        }
    }
}
