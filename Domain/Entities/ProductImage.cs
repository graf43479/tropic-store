
namespace Domain.Entities
{
    public class ProductImage
    {
        public int ProductImageID { get; set; }
        
        public int ProductID { get; set; }
        
        public int Sequence { get; set; }
        
        public string ImageExt { get; set; }

        public virtual Product Product { get; set; }
    }
}
