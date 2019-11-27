using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Combres;
using Domain.Entities;
//using NLog;
//using StackExchange.Profiling;
//using StackExchange.Profiling.EntityFramework6;
using StackExchange.Profiling;
using StackExchange.Profiling.EntityFramework6;
using WebUI.Binders;
using WebUI.Infrastructure;
using WebUI.Infrastructure.Concrete;

namespace WebUI
{
    // Примечание: Инструкции по включению классического режима IIS6 или IIS7 
    // см. по ссылке http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : HttpApplication
    {
        
        //Логирование
      //  private static Logger logger = LogManager.GetCurrentClassLogger();
        //LogManager.GetCurrentClassLogger();
        
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());

        }

       

        protected void Application_Start()
        {
            MiniProfilerEF6.Initialize();
            //Убираем версию MVC 
            MvcHandler.DisableMvcResponseHeader = true;
           // HttpContext.Current.Response.Headers.Set("Last-Modified", DateTime.Now.AddDays(-10).ToUniversalTime().ToString("S"));
            //отключение лишних движков представлений
            ViewEngines.Engines.Clear();
            ViewEngines.Engines.Add(new RazorViewEngine());

            //Уменьшение времени поиска файлов
            foreach (var viewEngine in ViewEngines.Engines.OfType<VirtualPathProviderViewEngine>())
            {
                viewEngine.ViewLocationCache = new DefaultViewLocationCache(TimeSpan.FromHours(24));
            } 

            //LogManager.GetCurrentClassLogger();
           // logger.Info("Application start");
            AreaRegistration.RegisterAllAreas();

          //  RouteTable.Routes.AddCombresRoute("Combres");
            
            RegisterGlobalFilters(GlobalFilters.Filters);
            
            RouteConfig.RegisterRoutes(RouteTable.Routes);

            
            ControllerBuilder.Current.SetControllerFactory(new NinjectControllerFactory());
            ModelBinders.Binders.Add(typeof(Cart),new CartModelBinder());
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            BundleTable.EnableOptimizations = true;
          //  AuthConfig.RegisterAuth();
            

           /* String connStr = System.Configuration.ConfigurationManager.ConnectionStrings["EFDbContext"].ConnectionString;
            System.Web.Caching.SqlCacheDependencyAdmin.EnableNotifications(connStr);
            System.Web.Caching.SqlCacheDependencyAdmin.EnableTableForNotifications(connStr, "Categories");*/
            //System.Web.Caching.SqlCacheDependencyAdmin.EnableTableForNotifications(connStr, "Albums");

            //  LayoutRendererFactory.AddLayoutRenderer("utc_date", typeof(UtcDateRenderer));
           // LayoutRendererFactory.AddLayoutRenderer("web_variables", typeof(WebVariablesRenderer));
 
            //DependencyResolver.SetResolver(new NinjectDependencyResolver());
        }


        protected void Application_BeginRequest()
        {
         if (Request.IsLocal)
MiniProfiler.Start();
        }

        protected void Application_EndRequest()
        {
           MiniProfiler.Stop();
        }

        public void Init()
        {
          //  logger.Info("Application Init");
        }



        public override string GetVaryByCustomString(HttpContext context, string arg)
        {
            // Проверить запрашиваемый тип кэширования
            if (arg == "browser")
            {
                // Определить текущий браузер
                string browserName;
                browserName = Context.Request.Browser.Browser;
                browserName += Context.Request.Browser.MajorVersion.ToString();

                // Указать, что эта строка должна применяться для варьирования кэша
                return browserName;
            }
            else
            {
                return base.GetVaryByCustomString(context, arg);
            }
        }
        


        public void Dispose()
        {
          //  logger.Info("Application Dispose");
        }

        protected void Application_Error()
        {
            Exception exception = Server.GetLastError();
            /*
            if (exception is InvalidOperationException)
            {
                throw new HttpException(404, "Страница не найдена");
            }
            */

            var z = Response.StatusCode;
            if (exception is HttpException)
            {
                if (z == 500)
                {
                    Response.Redirect(String.Format("~/Error/Http500"));
                }
            }
            else
            {
                if (exception is InvalidOperationException)
                {
                    //  Response.StatusCode = (int) HttpStatusCode.NotFound;
                    //  Response.Redirect(String.Format("~/Error/Http404/?url={0}", Request.Url.AbsolutePath));
                    Response.Redirect(String.Format("~/Error/Http404/?url={0}", Request.Url.AbsolutePath));
                    //   Server.Transfer("~/Error/Http404");
                }    
            }
            
          

            /*
            if (exception is HttpException)
            {
                
                if (((HttpException)(exception)).GetHttpCode()==404)
                {
                //    Response.StatusCode = (int) HttpStatusCode.NotFound;
                   // Response.Redirect(String.Format("~/Account/{0}/?message={1}", "UserEnter", exception.Message));
                    //throw new HttpException(404, "Страница не найдена");
                    //Response.Redirect(String.Format("~/{0}", "Error", exception.Message));
                  //  Response.StatusCode = (int) HttpStatusCode.NotFound;
                   // Response.Redirect(String.Format("~/Error/Http404/?url={0}", Request.Url.AbsolutePath));
                   // Server.Transfer("~/Error/Http404");
                   // Server.Transfer("~/404.htm");
                    //Server.ClearError();
                    //Response.Redirect(String.Format("~/{0}", "Error", exception.Message));
                    //Response.Redirect(String.Format("~/Account/{0}/?message={1}", "Error", exception.Message));
                    //Response.Redirect(String.Format("~/Account/Error"));
                   // Server.Transfer("~/Account/Error");
                    logger.Fatal("" + exception.Message);
                }
            }
            else
            {
              //  HttpException exp = new HttpException();
              //  var s = exp.GetHttpCode();
            }
            
            */
            Response.Clear();
            /*
            HttpException httpException = exception as HttpException;

            if (httpException != null)
            {
                string action;

                switch (httpException.GetHttpCode())
                {
                    case 404:
                        // page not found
                        action = "HttpError404";
                        break;
                    case 500:
                        // server error
                        action = "HttpError500";
                        break;
                    default:
                        action = "UserEnter";
                        break;
                }

                // clear error on server
                Server.ClearError();
                Response.Redirect(String.Format("~/Account/{0}/?message={1}", action, exception.Message));
                logger.Fatal(""+exception.Message);
            }
             * */
           // logger.Fatal("" + exception.Message);
        }



        protected void Application_End()
        {
          //  logger.Info("Application End");
        }
    }
}