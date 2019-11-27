using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;


namespace WebUI.Models
{
    public class OrderViewModel
    {
        public int OrderID { get; set; }
        public int UserID { get; set; }

        public string UserName { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public int OrderNumber { get; set; }
        public string UserAddress { get; set; }
        
        [DisplayFormat(DataFormatString = "{0:d}", ApplyFormatInEditMode = true)] //не работает
        public DateTime TransactionDate { get; set; }
        
        public decimal TotalValue { get; set; }
        
        [DefaultValue(true)]
        public bool IsActive { get; set; }

        //public IEnumerable<OrderSummary> OrdersSummary { get; set; }  
    }
}



/*
  public class ProductsListViewModel
    {
        public IEnumerable<Product> Products { get; set; }
        public PagingInfo PagingInfo { get; set; }
        public string CurrentCategory { get; set; }
    }
 */


/*
   public string UserName { get; set; }
            public string Phone { get; set; }
            public string Email { get; set; }
            public int OrderNumber { get; set; }
            public string UserAddress { get; set; }
            public DateTime TransactionDate { get; set; }
            public decimal TotalValue { get; set; }
 */