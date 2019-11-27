using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web.UI;
using Domain.Abstract;
using Domain.Entities;
using WebUI.Models;

namespace WebUI.Controllers
{
    public class NavController : Controller
    {
        private IProductRepository productRepository;
        private ICategoryRepository categoryRepository;
        private ISuperCategoryRepository superCategoryRepository;

        public NavController(IProductRepository productRepository, ICategoryRepository categoryRepository, ISuperCategoryRepository superCategoryRepository)
        {
            this.productRepository = productRepository;
            this.categoryRepository = categoryRepository;
            this.superCategoryRepository = superCategoryRepository;
        }

        //[OutputCache(Duration = 600, Location = OutputCacheLocation.None)] /*, Location = OutputCacheLocation.None)]*/
      //  [ChildActionOnly]
      //  [OutputCache(Duration = 1000)]
         public PartialViewResult Menu2(string category=null)
        {
            ViewBag.SelectedCategory = category;
           
            IEnumerable<Product> products =
                productRepository.Products.Where(
                    x => x.IsDeleted == false && x.IsActive == true && x.Category.IsDeleted == false &&x.Quantity!=0 &&x.Category.ShortName!=null).ToList();
             
             /*CategoryUrlFriendlyModel categoryUrlFriendlyModel = new CategoryUrlFriendlyModel()
                 {
                     Name = products.Select(x=>x.Category.Name.TrimEnd()
                 }*/


            var categories = (from p in products.ToList()
                              select new CategoryUrlFriendlyModel
                                  {
                                      Name = p.Category.Name.TrimEnd(),
                                      ShortName = p.Category.ShortName.TrimEnd(),
                                      CategoryID = p.Category.CategoryID,
                                      SuperCategoryID = p.Category.SuperCategoryID,
                                      IsDeleted = p.IsDeleted,
                                      SuperCategoryName = p.Category.SuperCategory.Name,
                                      SuperCategorySequence = p.Category.SuperCategory.Sequence
                                      //SuperCategorySequence = p.Category.SuperCategory.Sequence
                                  }).GroupBy(g => new {g.Name, g.ShortName /*, g.SuperCategorySequence*/})
                                    .Select(s => s.FirstOrDefault()).ToList();

             categories = categories.Where(x => x.IsDeleted == false).ToList();

            IEnumerable<SuperCategoriesViewModel> superCategories = (from tmp in categories
                                                                    select new SuperCategoriesViewModel
                                                                        {
                                                                            categories = categories.Where(x=>x.SuperCategoryID==tmp.SuperCategoryID).ToList(),
                                                                            superCategory = tmp.SuperCategoryName,
                                                                            Sequence = tmp.SuperCategorySequence
                                                                        }).GroupBy(g=> new {g.superCategory}).Select(s=>s.FirstOrDefault());
            superCategories = superCategories.Distinct().OrderBy(x=>x.Sequence);



            IEnumerable<SuperCategory> allSuperCategories = superCategoryRepository.SuperCategories.ToList();
             IEnumerable<Category> allCategories =
                 categoryRepository.Categories.Where(x => x.IsDeleted == false && x.ShortName != null).ToList();
            IEnumerable<Product> allProducts = productRepository.Products.Where(x => x.IsDeleted == false && x.IsActive == true && x.Quantity!=0 ).ToList();

             

             //IEnumerable<SuperCategoriesViewModel> 
            IEnumerable<CategoryUrlFriendlyModel> categoriesUrlFriendly = (from p in allProducts
                                                                       join c in allCategories on p.CategoryID equals
                                                                           c.CategoryID
                                                                       join s in allSuperCategories on c.SuperCategoryID
                                                                           equals s.SuperCategoryID

                                                                       select new CategoryUrlFriendlyModel
                                                                           {
                                                                               Name = c.Name.TrimEnd(),
                                                                               ShortName = c.ShortName.TrimEnd(),
                                                                               CategoryID = c.CategoryID,
                                                                               SuperCategoryID = s.SuperCategoryID,
                                                                               IsDeleted = p.IsDeleted,
                                                                               SuperCategoryName = s.Name,
                                                                               SuperCategorySequence = s.Sequence
                                                                           }).GroupBy(g => new {g.SuperCategoryName})
                                                                       .Select(s => s.FirstOrDefault()).ToList();



            IEnumerable<SuperCategoriesViewModel> superCategories2 = (from tmp in categoriesUrlFriendly
                                                                     select new SuperCategoriesViewModel
                                                                     {
                                                                         categories = categoriesUrlFriendly.Where(x=>x.SuperCategoryID==tmp.SuperCategoryID),//categories.Where(x => x.SuperCategoryID == tmp.SuperCategoryID).ToList(),
                                                                         superCategory = tmp.SuperCategoryName,
                                                                         Sequence = tmp.SuperCategorySequence
                                                                     }).GroupBy(g => new { g.superCategory }).Select(s => s.FirstOrDefault());
            //superCategories = superCategories.Distinct().OrderBy(x => x.Sequence);
             


            //IEnumerable<CategoryUrlFriendlyModel>

            return PartialView(superCategories2);

        }

        [OutputCache(Duration = 1000)]
         public ActionResult Menu(string category = null)
        {
            /*IEnumerable<Product> products =
               productRepository.Products.Where(
                   x => x.IsDeleted == false && x.IsActive == true && x.Category.IsDeleted == false && x.Quantity != 0 && x.Category.ShortName != null).ToList();
             */

            var categoryListAsync = categoryRepository.Categories.ToList();//.GetCategoryListAsync();
            var productListAsync = productRepository.Products.ToList(); // GetProductListAsync();

            IEnumerable<SuperCategory> allSuperCategories = superCategoryRepository.SuperCategories.ToList();
            //categoryListAsync.Wait();
            IEnumerable<Category> allCategories = categoryListAsync.Where(x => x.IsDeleted == false && x.ShortName != null).ToList();
                //categoryListAsync.Result.Where(x => x.IsDeleted == false && x.ShortName != null).ToList();

            //productListAsync.Wait();
            IEnumerable<Product> allProducts = productListAsync.Where(x => x.IsDeleted == false && x.IsActive == true && x.Quantity > 0 /*&& x.CategoryID==60*/).ToList();
            //IEnumerable<Product> allProducts = productListAsync.Result.Where(x => x.IsDeleted == false && x.IsActive == true && x.Quantity > 0 /*&& x.CategoryID==60*/).ToList();

             var mp = from p in allProducts
                      join c in allCategories on p.CategoryID equals c.CategoryID
                      join s in allSuperCategories on c.SuperCategoryID equals
                          s.SuperCategoryID
                      select c;
             
             


             IEnumerable<MenuViewModel> viewModels = (from p in allProducts
                                                      join c in allCategories on p.CategoryID equals c.CategoryID
                                                      join s in allSuperCategories on c.SuperCategoryID equals
                                                          s.SuperCategoryID
                                                      select new MenuViewModel()
                                                          {
                                                              SuperCategory = s,
                                                              //Categories = s.Categories.Where(x=>x.SuperCategoryID==s.SuperCategoryID && x.CategoryID==p.CategoryID).OrderBy(x=>x.Sequence),
                                                              Categories = mp.Where(x => x.SuperCategoryID == s.SuperCategoryID).OrderBy(x => x.Sequence).Distinct(),
                                                                  
                                                          }).GroupBy(g => new { g.SuperCategory}).Select(s => s.FirstOrDefault()).OrderBy(x=>x.SuperCategory.Sequence);


            return View(viewModels);
        }



    }
    
    }
