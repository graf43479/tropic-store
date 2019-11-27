using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;

namespace Domain.Abstract
{
    public interface IProductImageRepository
    {
        IQueryable<ProductImage> ProductImages { get; }

        void SaveProductImage(ProductImage productImage);

        void DeleteProductImage(ProductImage productImage);

        void DeleteProductImageBulk(IEnumerable<ProductImage> productImage);

        void ProductImageSequence(int productImageID, int productID, string actionType);

        void UpdateSequence(int productId, bool every);

    //    Task<IEnumerable<ProductImage>> GetProductImageListAsync();
    }

    
    
}
