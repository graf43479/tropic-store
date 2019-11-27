using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Caching;
using System.Web.Mvc;
using System.Web.UI;
using Domain;
using Domain.Abstract;
using Domain.Concrete;
using Domain.Entities;
using WebUI.Infrastructure.Concrete;
using WebUI.Models;
using System.Threading.Tasks;

namespace WebUI.Controllers
{
    public class ProductController : Controller
    {
        public int PageSize = Constants.PRODUCT_PAGE_SIZE;
        private IProductRepository productRepository;
        private ICategoryRepository categoryRepository;
        /*private IProductImageRepository productImageRepository;*/
        private IOrderDetailsRepository orderDetailsRepository;
     //   private INewsTapeRepository newsTapeRepository;

        public ProductController(IProductRepository productRepository, ICategoryRepository categoryRepository,/* IProductImageRepository productImageRepository,*/
            IOrderDetailsRepository orderDetailsRepository /*, INewsTapeRepository newsTapeRepository*/)
        {
            this.productRepository = productRepository;
            this.categoryRepository = categoryRepository;
            /*this.productImageRepository = productImageRepository;*/
            this.orderDetailsRepository = orderDetailsRepository;
          //  this.newsTapeRepository = newsTapeRepository;
        }

        //[OutputCache(Location=System.Web.UI.OutputCacheLocation.Any, Duration=60)]
       // [OutputCache(Duration = 5, VaryByParam = "*", VaryByCustom = "browser")]
        //[OutputCache(Duration = 60, Location = "Client")]
        //[OutputCache(Duration = 5000, Location = "Client")] /*, VaryByParam = "category"*/
        //[LastModifiedCacheFilter(Table = "Products", Column = "UpdateDate")]
     //   [OutputCache(Duration = 60, Location = OutputCacheLocation.None)] /*, Location = OutputCacheLocation.None)]*/
       // [OutputCache(CacheProfile = "SportStore")]
        //[OutputCache(Duration = 10)]
        //[OutputCache(Duration = 10000)]



     


        public async Task<ActionResult> List(string category, string sortOption, string searcher, int page = 1)
        {
         //   Thread.Sleep(10000);

//            #if DEBUG
//                Debug.Print("Номер текущего потока {0}", Thread.CurrentThread.ManagedThreadId);    
//#endif
            //if (HttpContext.IsDebuggingEnabled)
            //{
  
            //}
            
            Debug.WriteLine("Номер потока для ProductList {0}", Thread.CurrentThread.ManagedThreadId);
            var catgeoryByShortName = categoryRepository.GetCategoryByShortName(category);
          
            //Category cat = categoryRepository.GetCategoryByShortName(category);

            //Response.Cache.SetLastModified(cat.UpdateDate);
           // Response.ClearHeaders(); //RemoveOutputCacheItem();AddHeader("Last-Modified", cat.UpdateDate.ToString());
            //Response.AddHeader("Last-Modified", cat.UpdateDate.ToString());
            //Response.Cache.SetCacheability(HttpCacheability.Public);
            IEnumerable<Product> searchFilteredProductList2 = productRepository.Products.ToList();
            //var productList = productRepository.Products.Where(x => x.IsActive == true && x.IsDeleted == false);
            var searchFilteredProductList = searchFilteredProductList2.Where(
                p =>
                (category == null ||
                 p.Category.ShortName == category & p.Quantity != 0) &&
                (p.Name.ToUpper().Contains(searcher.ToUpper()) || p.Description.ToUpper().Contains(searcher.ToUpper()) || p.ArticleNumber.ToUpper().Contains(searcher.ToUpper()) || p.Category.Name.ToUpper().Contains(searcher.ToUpper())) && (p.IsActive == true && p.IsDeleted == false) && p.Quantity > 0);
                                                      

            if (sortOption==null)
            {
                if ((string)Session["sortOption"] == null)
                {
                    sortOption = null;
                }
                else
                {
                    sortOption = (string) Session["sortOption"];
                }
            }
            if (searcher==null)
            {
                searcher = "";
            }
            else
            {
                ViewBag.Searcher = searcher;
            }


          //  IEnumerable<ProductImage> productImagesList = productImageRepository.ProductImages;
            ProductsListViewModel viewModel = new ProductsListViewModel
                {
                   /*  Products = productRepository.Products
                     .Where(p=>category==null || p.Category.ShortName==category & p.Quantity!=0)
                                         .OrderBy(p => p.Sequence)
                                         .Skip((page - 1)*PageSize)
                                         .Take(PageSize),*/
             /*       Products = productRepository.Products
                    .Where(p=>category==null || p.Category.Name==category & p.Quantity!=0)
                                         .OrderBy(p => p.ProductID)
                                         .Skip((page - 1)*PageSize)
                                         .Take(PageSize),
              
              */ PagingInfo = new PagingInfo
                        {
                            CurrentPage = page,
                            ItemsPerPage = PageSize,
                          //  TotalItems = category == null ? productRepository.Products.Where(p => p.IsActive == true && p.IsDeleted == false).Count() : productRepository.Products.Where(e => (e.Category.ShortName == category) && (e.IsActive == true && e.IsDeleted == false)).Count()
                            TotalItems = category == null ? searchFilteredProductList.Count() : searchFilteredProductList.Where(e => e.Category.ShortName == category).Count()
                        },
                    CurrentCategory = category
                };


            //catgeoryByShortName.Wait();
            Category cat = catgeoryByShortName; //.Result; //catgeoryByShortNameAsync.Result;
            if (category!=null)
            {
                /*viewModel.Description = "Описание для " +
                                        categoryRepository.Categories.FirstOrDefault(x => x.ShortName == category).Name;*/
                //var s = categoryRepository.Categories.FirstOrDefault(x => x.ShortName == category).CategorySnippet;
                
                viewModel.Snippet = ViewBag.Snippet = ((cat==null) ? "" : cat.CategorySnippet);
                viewModel.Description = ViewBag.Description = ((cat == null) ? "" : cat.Description);
                ViewBag.Keywords = ((cat==null) ? "" : cat.CategoryKeywords) ;
                //ViewBag.Keywords = viewModel.
            }
            else if(searcher!="")
            {
                viewModel.Description = "Результаты поиска по запросу \"" + searcher+ "\"";
            }
            else
            {
                viewModel.Description = "Тайская фирма Tropicana - одна из самых передовых компаний в Юго-Восточной Азии, специализирующаяся " +
                                        "на производстве косметической и пищевой продукции, произведенной из кокосового масла высших проб. " +
                                        "Издавна обитатели этого региона высоко ценили полезные свойства кокосов и производных из них продуктов. " +
                                        "Побалуйте себя дарами тропической природы";
            }
            
            
            if (sortOption == "priceAsc")
            {
                
                Session["sortOption"] = "priceAsc";
                viewModel.Products = searchFilteredProductList.OrderBy(p => p.Price).Skip((page - 1) * PageSize).Take(PageSize);

            }
            else if (sortOption=="priceDesc")
                {
                    Session["sortOption"] = "priceDesc";
                    viewModel.Products = searchFilteredProductList.OrderByDescending(p => p.Price).Skip((page - 1) * PageSize).Take(PageSize);
                }
            else if (sortOption=="dateAsc")  
                  {
                      Session["sortOption"] = "dateAsc";
                      viewModel.Products = searchFilteredProductList.OrderBy(p => p.StartDate).Skip((page - 1) * PageSize).Take(PageSize);
                  }
           else if (sortOption == "dateDesc")
                 {
                     Session["sortOption"] = "dateDesc";
                     viewModel.Products = searchFilteredProductList.OrderByDescending(p => p.StartDate).Skip((page - 1) * PageSize).Take(PageSize);
                 }
            else
           {
               viewModel.Products = searchFilteredProductList.Skip((page - 1) * PageSize).Take(PageSize);

           }

            if (Request.IsAjaxRequest())
            {
               // Thread.Sleep(10000);
                Response.Cache.SetCacheability(System.Web.HttpCacheability.NoCache);
                return PartialView("ListPartial", viewModel);
            }

            if (viewModel.Products.Count()==0 && searcher=="")
            {
                
                //Response.StatusCode = 404;
                //Response.StatusCode = (int)HttpStatusCode.NotFound;

                return RedirectToAction("Http404", "Error", new {url = Request.Url.AbsolutePath});

               // Database.DefaultConnectionFactory.CreateConnection("Data Source=127.0.0.1,1433;Initial Catalog=TropicStoreRepl;Persist Security Info=true;User ID=socialnetwork;Password =socialnetwork;");


                //throw new HttpException(404, "Страница не найдена");
                //return new ViewResult{ViewName = "Error"};
            }

            if (cat!=null)
            {
                //var tmp = Request.Headers;
                var headerValue = Request.Headers["If-Modified-Since"];
                if (headerValue != null)
                {
                    //Response.AddHeader("Last-Modified", cat.UpdateDate.ToString());
                    var modifiedSince = DateTime.Parse(headerValue).ToLocalTime();
                    if (modifiedSince >= cat.UpdateDate)
                    {
                        return new HttpStatusCodeResult(304, "Page has not been modified");
                    }
                }

                Response.Cache.SetCacheability(HttpCacheability.ServerAndPrivate);
                Response.Cache.SetMaxAge(TimeSpan.FromSeconds(0));
                Response.Cache.AppendCacheExtension("max-age=0");
            
                //this.Response.AddCacheItemDependency();
             //   Response.Cache.SetMaxAge(TimeSpan.FromSeconds(0));
                Response.Cache.SetLastModifiedFromFileDependencies();
                //Response.Cache.SetExpires(DateTime.Now.AddDays(1));
             //   Response.Cache.AppendCacheExtension("max-age=0");
            //    Response.Cache.SetCacheability(HttpCacheability.ServerAndPrivate);

                Response.AddHeader("Last-Modified", cat.UpdateDate.ToUniversalTime().ToString("R"));

                Response.Cache.SetLastModified(cat.UpdateDate.ToUniversalTime());

                //Response.Cache.SetLastModified(cat.UpdateDate.ToUniversalTime());
                //Response.Headers.Remove("Last-Modified");
                //Response.Headers.Add("Last-Modified", cat.UpdateDate.ToUniversalTime().ToString("R"));

                
                /*Response.Cache.SetLastModifiedFromFileDependencies();
                Response.Cache.SetETagFromFileDependencies();
                
                Response.Cache.SetLastModified(cat.UpdateDate.ToUniversalTime());*/
                Response.AppendHeader("Last-Modified", cat.UpdateDate.ToUniversalTime().ToString("R"));
                //HttpContext.Response.AddHeader("Last-Modified", cat.UpdateDate.ToUniversalTime().ToString("R"));
                 HttpContext.Response.Cache.SetLastModified(cat.UpdateDate.ToUniversalTime());  
                // Response.ClearHeaders();
                //Response.Headers.Add("Last-Modified", cat.UpdateDate.ToUniversalTime().ToString("R"));
                //Response.Headers.Set("Last-Modified", cat.UpdateDate.ToUniversalTime().ToString("R"));
                /*   Response.AddHeader("Last-Modified", cat.UpdateDate.ToUniversalTime().ToString("R"));
                Response.Cache.SetVaryByCustom("category");
                Response.AppendHeader("Last-Modified", cat.UpdateDate.ToUniversalTime().ToString("R"));
                Response.Cache.SetCacheability(HttpCacheability.Public);
                Response.Cache.SetExpires(Cache.NoAbsoluteExpiration);
                HttpContext.Request.Headers.Add("If-Modified-Since", cat.UpdateDate.ToUniversalTime().ToString("R"));
                HttpContext.Response.Cache.SetMaxAge(new TimeSpan(1, 0, 0));
                HttpContext.Response.Cache.SetLastModified(cat.UpdateDate.ToUniversalTime());*/
                //   HttpCachePolicyBase cache = HttpContext.Response.Cache;
                //    TimeSpan cacheDuration = TimeSpan.FromSeconds(5000);
                //cache.SetLastModified(cat.UpdateDate.ToUniversalTime());
                //Response.Headers.Set("Last-Modified", cat.UpdateDate.ToUniversalTime().ToString("R"));
                // Response.Cache.SetLastModified(cat.UpdateDate.ToUniversalTime());
                //  Response.Cache.SetLastModified(DateTime.Now);
                //  Response.ClearHeaders();
                //Response. Cache.SetLastModified(cat.UpdateDate.ToUniversalTime());
                //     Response.Cache.SetLastModifiedFromFileDependencies();
                //Response.Cache.SetLastModified(Headers.());Set("Last-Modified",  cat.UpdateDate.ToUniversalTime().ToString("R"));
                // this.Response.Cache.SetCacheability(System.Web.HttpCacheability.Public);
                //this.Response.Cache.SetCacheability(System.Web.HttpCacheability.Private);
                //  this.Response.Cache.SetMaxAge(new TimeSpan(1,0,0));
                //this.Response.Cache.SetLastModified(DateTime.Now);
                //Response.Cache.SetCacheability(System.Web.HttpCacheability.Server);

            }
            return View(viewModel);
        }

        public string GetShortName(string name, int maxID)
        {

            EFDbContext ef = new EFDbContext();
            {
                //Constants.Translit tr = new Constants.Translit();
                string s = Constants.TransliterateText(name);
                if (ef.Products.Any(x => x.ShortName == s))
                {
                    return s + maxID.ToString();
                }
                return s;
            }
        }

        //[OutputCache(Duration = 10, VaryByParam = "*", VaryByCustom = "browser")]
        //[OutputCache(Duration = 10)]
        public ActionResult ProductInfo(string category, string shortName)
        {

            /*Response.Cache.SetLastModifiedFromFileDependencies();
            Response.Cache.SetETagFromFileDependencies();
            Response.Cache.SetCacheability(HttpCacheability.ServerAndPrivate);
            Response.Cache.AppendCacheExtension("max-age=0");
            */
            var productListAsync = productRepository.Products;// GetProductListAsync();

            Category cat = categoryRepository.Categories.FirstOrDefault(x => x.ShortName == category);
            if (cat==null)
            {
                return RedirectToAction("Http404", "Error", new { url = Request.Url.AbsolutePath });
            }
            
            shortName = shortName.Remove(shortName.LastIndexOf("'"), 1);

            //productListAsync.Wait();
            Product pr = productListAsync.FirstOrDefault(x => x.ShortName == shortName);
            if (pr == null)
            {
                return RedirectToAction("Http404", "Error", new { url = Request.Url.AbsolutePath });
            }

            
            var headerValue = Request.Headers["If-Modified-Since"];
            if (headerValue!=null)
            {
                var modifiedSince = DateTime.Parse(headerValue).ToLocalTime();
                if (modifiedSince>=pr.UpdateDate)
                {
                    return new HttpStatusCodeResult(304, "Page has not been modified");
                }
            }
            //Response.AddHeader("Last-Modified", pr.UpdateDate.ToUniversalTime().ToString("R"));

            
           // var response = context.HttpContext.Response;
            //response.Cache.SetLastModified(pr.UpdateDate);
           // Response.Cache.SetLastModified(DateTime.Now.AddDays(-1d));

            //Response.Headers.Set("Last-Modified", pr.UpdateDate.ToUniversalTime().ToString("R"));
            
            //Response.Cache.SetExpires(DateTime.Now.AddDays(-15d));
            //Response.Cache.SetLastModified(DateTime.Now.AddDays(-15d));
            //HttpContext.Request.Headers.Add("Last-Modified", pr.UpdateDate.ToUniversalTime().ToString("R"));
              //Request.Headers.Add("Last-Modified", pr.UpdateDate.ToUniversalTime().ToString("R"));  
                //Server
                //Request/
            //Response.Remove("Last-Modified"); //AddHeader("HUY", pr.UpdateDate.ToUniversalTime().ToString("R"));
            //HttpContext.Response.Headers.Remove("Last-Modified");
            Response.AddHeader("Last-Modified", pr.UpdateDate.ToUniversalTime().ToString("R"));
          //  Response.AddHeader("Last-Modified1", pr.UpdateDate.ToUniversalTime().ToString("R"));
            //Response.Cache.SetLastModified(pr.UpdateDate.ToUniversalTime());

            Response.AddHeader("Last-Modified2", pr.UpdateDate.ToUniversalTime().ToString("R"));
         //   Response.Cache.SetLastModified(pr.UpdateDate);
        //    Response.AppendHeader("Last-Modified", pr.UpdateDate.ToString("R"));
            return View(pr);
        }

        /*
        public FileContentResult GetImage(int productId)
        {
            Product prod = repository.Products.FirstOrDefault(p => p.ProductID == productId);
            if (prod != null)
            {
                return File(prod.ImageData, prod.ImageMimeType);
            }
            else
            {
                return null;
            }
        } */


        [OutputCache(Duration = 60, VaryByParam = "*", VaryByCustom = "browser")]
        public ActionResult ProductImagePreview(int ProductID, int start)
        {
            Product product = productRepository.Products.FirstOrDefault(x=>x.ProductID==ProductID);
            /*ViewBag.LeftArrow = start - 1;
            if ((start + Constants.PRODUCT_IMAGE_PREVIEW_COUNT) < product.ProductImages.Where(x=>x.ProductID==ProductID).Count() ) ViewBag.RightArrow = start + 1;
            else ViewBag.RightArrow = -1;
            ViewBag.ProductID = ProductID;
            */
            var images = product.ProductImages.Where(x => x.ProductID == product.ProductID)/*.OrderByDescending(item => item.Sequence).Skip(start).Take(Constants.PRODUCT_IMAGE_PREVIEW_COUNT)*/;
            return PartialView("_ProductImagePreview", images);
        }

        
        [HttpPost]
        public ActionResult SearchResult(string searcherEditor)
        {
            ViewBag.Searcher = searcherEditor;
           // Category cat = categoryRepository.GetCategoryByShortName(category);
            //  IEnumerable<ProductImage> productImagesList = productImageRepository.ProductImages;

       /*     var productList =
                productRepository.Products.Where(
                    (p => p.Quantity != 0 & p.Name.TrimEnd().Contains(searcherEditor.TrimEnd())
                          || p.Description.TrimEnd().Contains(searcherEditor.TrimEnd())
                          || p.Category.Name.TrimEnd().Contains(searcherEditor.TrimEnd()))).ToList();

            ProductsListViewModel viewModel = new ProductsListViewModel
            {
                Products = productList
                                    .OrderBy(p => p.ProductID)
                                    .Take(PageSize),
                PagingInfo = new PagingInfo
                {
                    CurrentPage = 1,
                    ItemsPerPage = PageSize,
                    TotalItems = productList.Count()
                },
            };*/
            return RedirectToAction("List", new {searcher = searcherEditor});
            // return View(viewModel);
        }

        public ActionResult Search()
        {
            return PartialView();
        }

        
        [HttpGet]
        public ActionResult SearchResult(string searcherEditor, int page = 1)
        {
            ViewBag.Searcher = searcherEditor;
            // Category cat = categoryRepository.GetCategoryByShortName(category);
            //  IEnumerable<ProductImage> productImagesList = productImageRepository.ProductImages;

            ProductsListViewModel viewModel = new ProductsListViewModel
            {
                Products = productRepository.Products
                    //.Where((p=>p.Quantity != 0 & p.Name.TrimEnd()==searchModel.SearchString.TrimEnd()))
                    .Where((p => p.Quantity != 0 & p.Name.TrimEnd().Contains(searcherEditor.TrimEnd())
                    || p.Description.TrimEnd().Contains(searcherEditor.TrimEnd())
                    || p.ArticleNumber.TrimEnd().Contains(searcherEditor.TrimEnd())
                    || p.Category.Name.TrimEnd().Contains(searcherEditor.TrimEnd())))
                                    .OrderBy(p => p.ProductID)
                                    .Skip((page - 1) * PageSize)
                                    .Take(PageSize),
                
                PagingInfo = new PagingInfo
                {
                    CurrentPage = page,
                    ItemsPerPage = PageSize,
                    TotalItems = productRepository.Products
                        //.Where((p=>p.Quantity != 0 & p.Name.TrimEnd()==searchModel.SearchString.TrimEnd()))
                    .Where((p => p.Quantity != 0 & p.Name.TrimEnd().Contains(searcherEditor.TrimEnd())
                    || p.Description.TrimEnd().Contains(searcherEditor.TrimEnd())
                    || p.ArticleNumber.TrimEnd().Contains(searcherEditor.TrimEnd())
                    || p.Category.Name.TrimEnd().Contains(searcherEditor.TrimEnd()))).Count()
                },


                //CurrentCategory = category //,
                //   ProductImagesList = productImageRepository.ProductImages.First() //productImagesList

            };

            return View(viewModel);

        }

        //[OutputCache(Duration = 1260, VaryByParam = "*", VaryByCustom = "browser")]
        [OutputCache(Duration = 1260)]
        public ActionResult UsefulProducts(int productId)
        {
            var productListAsync = productRepository.Products.ToList();//GetProductListAsync();
            
            
            
            IEnumerable<OrderDetails> ods = orderDetailsRepository.OrdersDetails.ToList();
            //IEnumerable<Product> productList = productRepository.Products.ToList();
            var orderSummaryID = ods.Where(x => x.ProductID == productId).Select(x => x.OrderSummaryID);
            var usefulProducts = from od in ods
                                 where
                                     od.OrderSummaryID ==
                                     orderSummaryID.Where(x => x == od.OrderSummaryID).FirstOrDefault()
                                     && od.ProductID!=productId
                                     //Далее необходимо вставить ограничение по интервалу времени покупки сопутствующих товаров 
                                 orderby od.ProductID descending 
                                 group od by od.ProductID
                                 into info
                                 select new {ProductID = info.Key, SaleCount = info.Count()};
            usefulProducts = usefulProducts.Where(x => x.SaleCount >= Constants.SALE_PRODUCT_COUNT).OrderByDescending(x=>x.SaleCount).Take(6);

            List<Product> products = new List<Product>();
            //productListAsync.Wait();
            IEnumerable<Product> productList = productListAsync.ToList();
            foreach (var p in usefulProducts)
            {
                Product product = productList.Where(x => x.ProductID == p.ProductID).Single();
                if (product.IsActive == true && product.IsDeleted == false)
                {
                    products.Add(product);    
                }
            }
            return PartialView(products);
        }


        [HttpGet]
        public ActionResult ProductPreview(int productId)
        {
            Product product = productRepository.Products.FirstOrDefault(x => x.ProductID==productId);
            bool tmp = Request.IsAjaxRequest();
            return PartialView(product);
        }

    }
}


/*
       var categoryGroupping = from n in repositoryProduct.Products.ToList()
                     group n by n.CategoryID
                     into g
                     select new {CategoryID = g.Key, ProductCount = g.Count()};
 */