using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Web.Mvc;
using Domain.Abstract;
using Domain.Concrete;
using Domain.Entities;
using WebUI.Models;

namespace WebUI.Controllers
{
    public class ArticleController : Controller
    {
        //
        // GET: /Article/

        public int PageSize = 12;
        private IArticleRepository articleRepository;

        public ArticleController()
        {
        }

        public ArticleController(IArticleRepository articleRepository)
        {
            this.articleRepository = articleRepository;
        }
        [OutputCache(Duration = 1000)]
        public ActionResult ArticleTape()
        {
           var articles = articleRepository.Articles.OrderByDescending(x => x.ArticleDate).Take(2);
            if (Request.IsAjaxRequest())
            {
                //Thread.Sleep(2000);
                return PartialView(articles);
            }
            return PartialView(articles);
        }

        [OutputCache(Duration = 1000)]
        public ActionResult ArticlePreviews(int page = 1)
        {
            var articleRepositoryTmp = articleRepository.Articles.ToList();

            ArticlesListViewModel viewModel = new ArticlesListViewModel()
            {
                PagingInfo = new PagingInfo
                {
                    CurrentPage = page,
                    ItemsPerPage = PageSize,
                    //  TotalItems = category == null ? productRepository.Products.Where(p => p.IsActive == true && p.IsDeleted == false).Count() : productRepository.Products.Where(e => (e.Category.ShortName == category) && (e.IsActive == true && e.IsDeleted == false)).Count()
                    //TotalItems = category == null ? searchFilteredProductList.Count() : searchFilteredProductList.Where(e => e.Category.ShortName == category).Count()
                    TotalItems = articleRepositoryTmp.Count()//articleRepository.Articles.Count()
                },
                Articles = articleRepositoryTmp.OrderByDescending(p => p.ArticleDate).Skip((page - 1) * PageSize).Take(PageSize)
            };

            return View(viewModel);
            
        }

        public ActionResult Article(/*int articleId,*/ string shortLink)
        {
            Debug.Print("СООООООООООООООБЩЕНИЕЕЕЕЕЕЕЕЕЕЕ"); //WriteLine("Номер текущего потока ");  
            Article nt = articleRepository.Articles.FirstOrDefault(x => x.ShortLink == shortLink);

            if (nt == null)
            {
                return RedirectToAction("Http404", "Error", new { url = Request.Url.AbsolutePath });
            }

            var headerValue = Request.Headers["If-Modified-Since"];
            if (headerValue != null)
            {
                var modifiedSince = DateTime.Parse(headerValue).ToLocalTime();
                if (modifiedSince >= nt.UpdateDate)
                {
                    return new HttpStatusCodeResult(304, "Page has not been modified");
                }
            }
            Response.AddHeader("Last-Modified", nt.UpdateDate.ToUniversalTime().ToString("R"));

            //return null;
            return View(nt);
        }


       
        public PartialViewResult RandomArticle()
        {
            IEnumerable<Article> articles = articleRepository.Articles.OrderBy(x=>x.ArticleDate).ToList();
            if (articles.Count()<3)
            {
                return null;
            }
            else
            {
                int count = articles.Count() - 2;
                articles = articles.Take(count);
                int[] ids = new int[count];

                int i = 0;
                foreach (Article articleAr in articles)
                {
                    ids[i] = articleAr.ArticleID;
                    i++;
                }

                Random rnd = new Random();
                count = rnd.Next(0, count);
                count = ids[count] - 1;


                try
                {
                    Article article = articles.FirstOrDefault(x => x.ArticleID == ids[count]);
                    return PartialView("RandomArticle", article);    
                }
                catch (Exception)
                {
                    return null;
                }
                
            }
            
        }


    }

    
}



/*
  public class NewsController : Controller
    {
       
     
     

       

 */