using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    public class DimShipping
    {
        [Key]
        public int ShippingID { get; set; }
        public string ShippingType { get; set; }
        public string ShippingDesc { get; set; }
        public decimal ShippingPrice { get; set; }
        //public DateTime ExpluatationDate { get; set; }
        
        //public DateTime ExpirationDate { get; set; }
        public bool isActive { get; set; }

       // public virtual ICollection<ShippingDatails> ShippingDatailses { get; set; }
    }
}
