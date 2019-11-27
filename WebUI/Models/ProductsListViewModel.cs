using System.Collections.Generic;
using Domain.Entities;

namespace WebUI.Models
{
    public class ProductsListViewModel
    {
        public IEnumerable<Product> Products { get; set; }
        public PagingInfo PagingInfo { get; set; }
        public string CurrentCategory { get; set; }
        public string Description { get; set; }
        public string Snippet { get; set; }
        //   public ProductImage ProductImagesList { get; set; }
    }
}