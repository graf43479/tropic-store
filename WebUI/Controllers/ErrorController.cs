using System.Net;
using System.Web.Mvc;

namespace WebUI.Controllers
{
    public class ErrorController : Controller
    {
        //
        // GET: /Error/

        #region Http404

        public ActionResult Http404(string url)
        {
            
                Response.StatusCode = (int)HttpStatusCode.NotFound;
            
            
        /*    var model = new NotFoundViewModel();
          */  
      /*      // Если путь относительный ('NotFound' route), тогда его нужно заменить на запрошенный путь
            model.RequestedUrl = Request.Url.OriginalString.Contains(url) & Request.Url.OriginalString != url ?
                Request.Url.OriginalString : url;
            // Предотвращаем зацикливание при равенстве Referrer и Request
            model.ReferrerUrl = Request.UrlReferrer != null &&
                Request.UrlReferrer.OriginalString != model.RequestedUrl ?
                Request.UrlReferrer.OriginalString : null;
            */
            // TODO: добавить реализацию ILogger

            ViewBag.NotFoundUrl = "http://" + Request.Url.Host +  url;
            return View();
        }



        public ActionResult Http500()
        {

            //Response.StatusCode = (int)HttpStatusCode.;

            Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            return View();

        }

        public class NotFoundViewModel
        {
            public string RequestedUrl { get; set; }
            public string ReferrerUrl { get; set; }
        }

        #endregion


    }
}
