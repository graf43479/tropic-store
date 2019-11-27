using Domain.Entities;

namespace WebUI.Models
{
    public class CartIndexViewModel
    {
        public Cart Cart { get; set; }
        public string ReturnUrl { get; set; }
        public User user { get; set; }
        //public IEnumerable<DimShipping> dimShipping { get; set; }
     //   public virtual ICollection<DimShipping> dimShipping { get; set; }
        
    }
}