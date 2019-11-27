using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Domain.Abstract;
using Domain.Entities;
using EntityState = System.Data.Entity.EntityState;

namespace Domain.Concrete
{
    public class EFProductRepository : IProductRepository
    {
        private EFDbContext context;

       // private Func<> operation = null;
        
        
        public EFProductRepository(EFDbContext context)
        {
            this.context = context;
            
         }

        public IQueryable<Product> Products {
            get
            {
                Debug.WriteLine("Номер данного потока {0}", Thread.CurrentThread.ManagedThreadId);
                //return  context.Products.Include("Category").Include("ProductImages")   ;
                
                return  context.Products.Include("Category").Include("ProductImages");
            }
        }


        public Product GetProductOrigin(Product product)
        {
            //context.Entry(existingBlog).State = EntityState.Unchanged; 
           // Product val = context.Products.Single(x => x.ProductID == productId);
            context.Entry(product).State = EntityState.Detached;
            return product;
        }

        //public async  Task<IEnumerable<Product>> GetProductListAsync()
        //{
        //    return await context.Products.ToListAsync().ConfigureAwait(false);
        //}

        //public async Task<Product> GetProductByIDAsync(int productID)
        //{
        //    return await context.Products.FirstOrDefaultAsync(x => x.ProductID == productID);
        //}

        public void SaveProduct(Product product)
        {
            if (product.ProductID == 0)
            {
                try
                {
                    product.Sequence = context.Products.Where(x=>x.CategoryID==product.CategoryID).Select(x => x.Sequence).Max() + 1;
                }
                catch (Exception)
                {
                    product.Sequence = 1;
                }
                product.ShortName = GetShortName(product.Name, context.Products.Max(x => x.ProductID) + 1);
                context.Products.Add(product);
            }
            else
            {
                context.Entry(product).State=EntityState.Modified;  
            }

            context.SaveChanges();
        }



        public void RefreshEveryProductSequence(int[] categoryIdArray)
        {
              foreach (var i in categoryIdArray)
              {
                  IEnumerable<Product> productListNotActive = context.Products.Where(x => x.CategoryID == i).Where(z => z.IsActive != true || z.IsDeleted == true);
                foreach (var product in productListNotActive)
                {
                        product.Sequence = 10000;
                        context.Entry(product).State = EntityState.Modified;
                        
                   // Косяк!!! 
                        //SaveProduct(product);
                }
                context.SaveChanges();
              }
            


            foreach (var i in categoryIdArray)
            {
                int currentSequensNum = 1;

               //выставляем максимум тем товарам, у которых статус неактивен или удален
                


               IEnumerable<Product> productList = context.Products.Where(x => x.CategoryID == i).Where(z=>z.IsActive==true && z.IsDeleted==false).OrderBy(f=>f.Sequence);
               /*   foreach (var product in productList)
                 {
                     if (product.Sequence < currentSequensNum)
                     {
                         product.Sequence = 10000;
                         context.Entry(product).State = EntityState.Modified;
                     }
                 }
                 context.SaveChanges();
                 */
             //   productList = productList.OrderBy(x => x.Sequence);

                foreach (var product in productList)
                {
               /*     if (product.Sequence == currentSequensNum)
                    {
                        currentSequensNum++;    
                    }
                    else*/
                   
                        product.Sequence = currentSequensNum;
                        //SaveProduct(product);
                        context.Entry(product).State = EntityState.Modified;
                        
                        currentSequensNum++;
                }

                context.SaveChanges();
                
                

            }

            
        }


        public void DeleteProduct(Product product)
        {
            if (product.IsActive==false || product.IsDeleted==true)
            {
                try
                {
                    context.Products.Remove(product);
                    context.SaveChanges();
                }
                catch (Exception)
                {
                    throw;
                }
            }
            else
            {
                try
                {
                    foreach (var p in context.Products.Where(x => x.CategoryID == product.CategoryID).OrderBy(x=>x.Sequence))
                    {
                        if (p.Sequence <= product.Sequence)
                        {

                        }
                        else
                        {
                            --p.Sequence;
                            context.Entry(p).State = EntityState.Modified;
                        }
                    }
                }
                catch (Exception)
                {

                }

                try
                {
                    context.Products.Remove(product);
                    context.SaveChanges();
                }
                catch (Exception)
                {
                    throw;
                }
            }
            
        }

        public string GetShortName(string name, int maxID)
        {

            EFDbContext ef = new EFDbContext();
            {
                string s = Constants.TransliterateText(name);
                if (ef.Products.Any(x => x.ShortName == s && x.ProductID!=maxID))
                {
                    return s + maxID.ToString();
                }
                return s;
            }
            
         
        }

        public void RefreshAllShortNames()
        {
            for (int i = 0; i < 2; i++)
            {
                foreach (var product in context.Products)
                {
                    product.ShortName = GetShortName(product.Name, product.ProductID);
                    context.Entry(product).State = EntityState.Modified;
                }
                context.SaveChanges();     
            }
        }

        public void RefreshProductShortName(Product product)
        {
                product.ShortName = GetShortName(product.Name, product.ProductID);
                context.Entry(product).State = EntityState.Modified;
                context.SaveChanges();
        }


           
        public void ProductSequence(int productId, string actionType)
        {
            Product product1 =
                context.Products.FirstOrDefault(x => x.ProductID == productId);

            if (actionType == "Up")
            {
                if (product1.Sequence == 1)
                {
                    return;
                }
                else
                {
                    Product product2 =
                    context.Products.Where(x=>x.CategoryID==product1.CategoryID).FirstOrDefault(x => x.Sequence == product1.Sequence - 1);
                    product2.Sequence = product2.Sequence + 1;
                    product1.Sequence = product1.Sequence - 1;
                    context.Entry(product1).State = EntityState.Modified;
                    context.Entry(product2).State = EntityState.Modified;
                    context.SaveChanges();
                }
            }
            else if (actionType == "Down")
            {
                if (product1.Sequence == context.Products.Where(x=>x.CategoryID==product1.CategoryID).Max(x => x.Sequence))
                {
                    return;
                }
                else
                {
                    Product product2 =
                context.Products.Where(x=>x.CategoryID==product1.CategoryID).FirstOrDefault(x => x.Sequence == product1.Sequence + 1);
                    product2.Sequence = product2.Sequence - 1;
                    product1.Sequence = product1.Sequence + 1;
                    context.Entry(product1).State = EntityState.Modified;
                    context.Entry(product2).State = EntityState.Modified;
                    context.SaveChanges();
                }
            }
        }

        public void SetActiveStatus(bool isActive, Product product)
        {
            product.IsActive = isActive;
            context.Entry(product).State = EntityState.Modified;
            context.SaveChanges();
            int[] categoryArray = new int[1];
            categoryArray[0] = product.CategoryID;
            RefreshEveryProductSequence(categoryArray);
        }

        public void SetDeletedStatus(bool isDeleted, Product product)
        {
            product.IsDeleted = isDeleted;
            product.Sequence = 10000;
            context.Entry(product).State = EntityState.Modified;
            context.SaveChanges();
            int[] categoryArray = new int[1];
            categoryArray[0] = product.CategoryID;
            RefreshEveryProductSequence(categoryArray);
            //erer


/*
 foreach (var i in categoryIdArray)
            {
                int currentSequensNum = 1;
                IEnumerable<Product> productList = context.Products.Where(x => x.CategoryID == i).Where(z=>z.IsActive==true && z.IsDeleted==false).OrderBy(f=>f.Sequence);
                foreach (var product in productList)
                {
                    if (product.Sequence < currentSequensNum)
                    {
                        product.Sequence = 10000;
                    }
                }

                productList = productList.OrderBy(x => x.Sequence);

                foreach (var product in productList)
                {
                    if (product.Sequence == currentSequensNum)
                    {
                        currentSequensNum++;    
                    }
                    else
                    {
                        product.Sequence = currentSequensNum;
                        SaveProduct(product);
                        currentSequensNum++; 
                    }
                }

            }
 */


        }


        public void UpdateProductSequence(int categoryId, bool every)
        {
            int i = 1;
            if (every)
            {
                int[] z =
                context.Products.Select(x => x.CategoryID).Distinct().ToArray();

                foreach (var category in z)
                {
                    foreach (var product in context.Products.Where(x=>x.CategoryID==category))
                    {
                        product.Sequence = i;
                        context.Entry(product).State=EntityState.Modified;
                        i++;
                    }
                }
            }
            else
            {
                foreach (var product in context.Products.Where(x=>x.CategoryID==categoryId))
                {
                    product.Sequence = i;
                    context.Entry(product).State = EntityState.Modified;
                    i++;  
                }
            
            }
            context.SaveChanges();
        }

        /*
        public async Task<bool> SaveProductAsync(Product product)
        {
            if (product.ProductID == 0)
            {
                try
                {
                    product.Sequence = context.Products.Where(x => x.CategoryID == product.CategoryID).Select(x => x.Sequence).Max() + 1;
                }
                catch (Exception)
                {
                    product.Sequence = 1;
                }
                product.ShortName = GetShortName(product.Name, context.Products.Max(x => x.ProductID) + 1);
                context.Products.Add(product);
            }
            else
            {
                context.Entry(product).State = EntityState.Modified;
            }

            await context.SaveChangesAsync();
            return true;
        }
        */



        //public void SaveProduct(Product product)
        //{
        //    if (product.ProductID == 0)
        //    {
        //        try
        //        {
        //            product.Sequence = context.Products.Where(x => x.CategoryID == product.CategoryID).Select(x => x.Sequence).Max() + 1;
        //        }
        //        catch (Exception)
        //        {
        //            product.Sequence = 1;
        //        }
        //        product.ShortName = GetShortName(product.Name, context.Products.Max(x => x.ProductID) + 1);
        //        context.Products.Add(product);
        //    }
        //    else
        //    {
        //        context.Entry(product).State = EntityState.Modified;
        //    }

        //    context.SaveChanges();
        //}

    }
}
