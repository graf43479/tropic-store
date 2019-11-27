using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebUI.Infrastructure.Concrete
{
    public class LastModifiedCacheFilterAttribute : ActionFilterAttribute
    {
        public string Table { get; set; }
        public string Column { get; set; }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var lastModified = DateTime.Now; //= read the value from the passed Column/Table and set it here 

            var ifModifiedSinceHeader = filterContext.RequestContext.HttpContext.Request.Headers["If-Modified-Since"];

            if (!String.IsNullOrEmpty(ifModifiedSinceHeader))
            {
                var modifiedSince = DateTime.Parse(ifModifiedSinceHeader).ToLocalTime();
                if (modifiedSince >= lastModified)
                {
                    filterContext.Result = new EmptyResult();
                    filterContext.RequestContext.HttpContext.Response.Cache.SetLastModified(lastModified.ToUniversalTime());
                    filterContext.RequestContext.HttpContext.Response.StatusCode = 304;
                }
            }

            base.OnActionExecuting(filterContext);
        }

        public override void OnResultExecuted(ResultExecutedContext filterContext)
        {
            // var lastModified = read the value from the passed Column/Table and set it herefilterContext.RequestContext.HttpContext.Response.Cache.SetLastModified(lastModified.ToUniversalTime());
            base.OnResultExecuted(filterContext);
        }
    }
}