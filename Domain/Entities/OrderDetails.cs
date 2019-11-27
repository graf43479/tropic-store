
namespace Domain.Entities
{
    public class OrderDetails
    {
      //  [Required]
       // [HiddenInput(DisplayValue = false)]
        //[Key]
        public int OrderDetailsID { get; set; }

        //[ForeignKey("Products")] 
         public int ProductID { get; set; }

         public decimal Price { get; set; }

         public int Quantity { get; set; }

         //[Required(ErrorMessage = "*")]
         public int OrderSummaryID { get; set; }
        
        //[Required(ErrorMessage = "*")]
         public int UserID { get; set; }

        // public List<Product> Product { get; set; }   
        // [ForeignKey("ProductID")]
         public virtual Product Product { get; set; }


         public virtual OrdersSummary OrdersSummary { get; set; }
         //public virtual Category Category { get; set; }   
        // public virtual ICollection<Product> Products { get; set; }


    }
}
