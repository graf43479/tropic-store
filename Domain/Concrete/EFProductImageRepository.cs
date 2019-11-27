using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using Domain.Abstract;
using Domain.Entities;
using EntityState = System.Data.Entity.EntityState;

namespace Domain.Concrete
{
    public class EFProductImageRepository: IProductImageRepository 
    {
        private EFDbContext context;

        public EFProductImageRepository(EFDbContext context)
        {
            this.context = context;
        }

        public IQueryable<ProductImage> ProductImages {
            get
            {
                return context.ProductImages.Include("Product");
            }
        }


        public void SaveProductImage(ProductImage productImage)
        {
            if (productImage.ProductImageID == 0)
            {
                context.ProductImages.Add(productImage);
            }
            else
            {
                context.Entry(productImage).State = EntityState.Modified;
            }
            context.SaveChanges();
        }

        public void DeleteProductImage(ProductImage productImage)
        {
            context.ProductImages.Remove(productImage);
            context.SaveChanges();
        }

        public void DeleteProductImageBulk(IEnumerable<ProductImage> productImage)
        {
            //context.ProductImages.Remove(productImage);
            foreach (var image in productImage)
            {
                context.ProductImages.Remove(image);
            }
            context.SaveChanges();
        }

        public void ProductImageSequence(int productImageID, int productID, string actionType)
        {
            int pi1, pi2;
            ProductImage productImage1 = context.ProductImages.FirstOrDefault(x => x.ProductImageID == productImageID);

            if (actionType == "Up")
            {
                if (productImage1.Sequence==1) 
                {
                    //return;
                }
                else
                {
                ProductImage productImage2 =
                    context.ProductImages.Where(x=>x.ProductID==productID).FirstOrDefault(x => x.Sequence == productImage1.Sequence - 1);
                    pi1 = productImage1.Sequence - 1;
                    pi2 = productImage2.Sequence + 1;
                    productImage2.Sequence = pi2; //productImage2.Sequence + 1;
                    productImage1.Sequence = pi1; //productImage1.Sequence - 1;
                    context.Entry(productImage1).State = EntityState.Modified;
                    context.Entry(productImage2).State = EntityState.Modified;
                    context.SaveChanges();
                    //return;
                }
              }
            else if (actionType == "Down")
            {
                if (productImage1.Sequence==context.ProductImages.Max(x=>x.Sequence))
                {
                    //return;
                }
                else
                {
                ProductImage productImage2 =
                context.ProductImages.Where(x=>x.ProductID==productID).FirstOrDefault(x => x.Sequence == productImage1.Sequence +1);
                pi1 = productImage1.Sequence + 1;
                pi2 = productImage2.Sequence - 1;
                    productImage2.Sequence = pi2; // productImage2.Sequence - 1;
                    productImage1.Sequence = pi1; //productImage1.Sequence + 1;
                    
                context.Entry(productImage2).State = EntityState.Modified;
                context.Entry(productImage1).State = EntityState.Modified; 
                
                context.SaveChanges();
                    //return;
                }
            }
            
        }

        public void UpdateSequence(int productId, bool every)
        {
            
        if (every)
        {
            int[] z = context.ProductImages.Select(x => x.ProductID).Distinct().ToArray();
            
            foreach (var p in z)
            {
                int i = 1;
                //context.ProductImages.Where(x=>x.ProductID==p).Select(x=>x.ProductID);
                foreach (var productImage in context.ProductImages.Where(x=>x.ProductID==p))
                {
                    productImage.Sequence = i;
                    context.Entry(productImage).State = EntityState.Modified;
                    i++;
                }
            }
           }

        else
        {
            int i = 1;
            foreach (var productImage in context.ProductImages.Where(x=>x.ProductID==productId))
            {
                productImage.Sequence = i;
                context.Entry(productImage).State = EntityState.Modified;
                i++;
            }
        }
        context.SaveChanges();


        /*    
            try
            {
                for (int i = 1; i <= context.ProductImages.Count(); i++)
                {
                    context.ProductImages.FirstOrDefault();
                }
            }
            catch (Exception)
            {
                
                throw;
            }
         * 
         */
        }

        //public async Task<IEnumerable<ProductImage>> GetProductImageListAsync()
        //{
        //    return await context.ProductImages.ToListAsync().ConfigureAwait(false);
        //}
    }
}



