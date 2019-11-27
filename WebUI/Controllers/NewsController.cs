using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Mvc;
using Domain.Abstract;
using Domain.Entities;
using WebUI.Models;

namespace WebUI.Controllers
{
    public class NewsController : Controller
    {
        public int PageSize = 12;
        private INewsTapeRepository newsTapeRepository;

        public NewsController(INewsTapeRepository newsTapeRepository)
        {
            this.newsTapeRepository = newsTapeRepository;
        }

        [OutputCache(Duration = 1000)]
        [HttpGet]
        public async Task<ActionResult> NewsTape()
        {
          //  var news = newsTapeRepository.NewsTapes.OrderByDescending(x => x.NewsDate).Take(3).ToList();
          //  Debug.WriteLine("Применяет пото №{0}", Thread.CurrentThread.ManagedThreadId);
            IEnumerable<NewsTape> news =
                await Task.Run(() => newsTapeRepository.NewsTapes.OrderByDescending(x => x.NewsDate).Take(3).ToList());
         //   Debug.WriteLine("Применяет пото №{0}", Thread.CurrentThread.ManagedThreadId);
            if (Request.IsAjaxRequest())
            {
              //  Thread.Sleep(2000);
                return PartialView(news);
            }
            return PartialView(news);
        }

        [OutputCache(Duration = 1000)]
        public ActionResult NewsPreviews(int page = 1)
        {
            
            //IEnumerable<NewsTape> news = newsTapeRepository.NewsTapes;

            //  IEnumerable<ProductImage> productImagesList = productImageRepository.ProductImages;
            NewsListViewModel viewModel = new NewsListViewModel()
            {
                PagingInfo = new PagingInfo
                {
                    CurrentPage = page,
                    ItemsPerPage = PageSize,
                    //  TotalItems = category == null ? productRepository.Products.Where(p => p.IsActive == true && p.IsDeleted == false).Count() : productRepository.Products.Where(e => (e.Category.ShortName == category) && (e.IsActive == true && e.IsDeleted == false)).Count()
                    //TotalItems = category == null ? searchFilteredProductList.Count() : searchFilteredProductList.Where(e => e.Category.ShortName == category).Count()
                    TotalItems = newsTapeRepository.NewsTapes.Count()
                },
            NewsTapes = newsTapeRepository.NewsTapes.OrderByDescending(p => p.NewsDate).Skip((page - 1) * PageSize).Take(PageSize)
            };
            


            return View(viewModel);
        }


        public ActionResult NewsArticle(int newsId)
        {
            NewsTape nt = newsTapeRepository.NewsTapes.FirstOrDefault(x => x.NewsID == newsId);

            if (nt == null)
            {
                return RedirectToAction("Http404", "Error", new { url = Request.Url.AbsolutePath });
            }

            var headerValue = Request.Headers["If-Modified-Since"];
            if (headerValue!=null)
            {
                var modifiedSince = DateTime.Parse(headerValue).ToLocalTime();
                if (modifiedSince>=nt.UpdateDate)
                {
                    return new HttpStatusCodeResult(304, "Page has not been modified");
                }
            }
            Response.AddHeader("Last-Modified", nt.UpdateDate.ToUniversalTime().ToString("R"));


            return View(nt);
        }


        public static string FindLink(string text)
        {
            return null;
        }

       

    }
}
