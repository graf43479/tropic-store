using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace WebUI
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            // routes.AddCombresRoute("Combres");
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            //---------------------------------------------------
            routes.MapRoute("Activate", "Account/Activate/{username}/{key}", new { controller = "Account", action = "Activate", username = UrlParameter.Optional, key = UrlParameter.Optional });
            //---------------------------------------------------
            routes.MapRoute("Robots.txt", "robots.txt", new { controller = "Account", action = "Robots" });
            //---------------------------------------------------
            routes.MapRoute("Sitemap.xml", "sitemap.xml", new { controller = "Account", action = "Sitemap" });
            //---------------------------------------------------
            routes.MapRoute(null, "Account/VkAuth/vk-auth/{key}", new { controller = "Account", action = "VkAuth", key = UrlParameter.Optional });
            //---------------------------------------------------
            routes.MapRoute(null, "Account/FbLogin/fblogin/{key}", new { controller = "Account", action = "FbLogin", key = UrlParameter.Optional });
            //---------------------------------------------------
            routes.MapRoute(null, "Contacts", new { controller = "Account", action = "Contacts" }); //contacts
            //---------------------------------------------------
            routes.MapRoute(null, "Delivery", new { controller = "Account", action = "Delivery" }); //delivery
            //---------------------------------------------------
            routes.MapRoute(null, "News", new { controller = "News", action = "NewsPreviews" }); //newsPreviews
            //---------------------------------------------------
            routes.MapRoute(null, "Articles", new { controller = "Article", action = "ArticlePreviews" }); //articlesPreviews
            //---------------------------------------------------
            routes.MapRoute(null, "Articles/{shortLink}", new { controller = "Article", action = "Article", shortLink = (string)null }); //articles
            //---------------------------------------------------
            routes.MapRoute(null, "{category}/{shortName}'", new { controller = "Product", action = "ProductInfo", category = (string)null, shortName = (string)null }); //!!!
            //----------------------------------------------------
            routes.MapRoute(null, "", new { controller = "Product", action = "List", category = (string)null, page = 1 });
            //----------------------------------------------------
            routes.MapRoute(null, "Page{page}", new { controller = "Product", action = "List", category = (string)null }, new { page = @"\d+" });
            //----------------------------------------------------
            routes.MapRoute(null, "{category}", new { controller = "Product", action = "List", page = 1 });
            //----------------------------------------------------

            routes.MapRoute(null, "Search{searchModel}/Page{page}", new { controller = "Product", action = "SearchResult" }, new { Searcher = (string)null, page = @"\d+" });
            //----------------------------------------------------

            routes.MapRoute(null, "{category}/Page{page}", new { controller = "Product", action = "List" }, new { page = @"\d+" });

            //----------------------------------------------------
            routes.MapRoute(null, "{controller}/{action}");
           
        }
    }
}