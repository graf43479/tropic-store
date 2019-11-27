using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace Domain.Entities
{
    public class OrdersSummary
    {
        //[HiddenInput(DisplayValue = false)]
       [Key]
        public int OrderSummaryID { get; set; }

       public int UserID { get; set; }

       public int OrderNumber { get; set; }
        
        [Required(ErrorMessage = "Введите имя клиента")]
        public string UserName { get; set; }

        [DataType(DataType.MultilineText)]
        [Required(ErrorMessage = "Введите адрес доставки")]
        public string UserAddress { get; set; }
        
        [Required(ErrorMessage = "Введите номер телефона")]
        public string Phone { get; set; }
        
        public string Email { get; set; }

        // [DisplayFormat(DataFormatString = "{0:d}", ApplyFormatInEditMode = true)]
        //[DataType(DataType.Date)]
        public DateTime TransactionDate { get; set; }
        
        public decimal TotalValue { get; set; }

        [DefaultValue(true)]
        public bool IsActive { get; set; }

        
        public string ShippingType { get; set; }

        public decimal? ShippingPrice { get; set; }
        

        //public virtual ICollection<DimOrderStatus> DimOrderStatuses { get; set; }  
        public virtual ICollection<OrderStatus> OrderStatuses { get; set; }

        public virtual ICollection<OrderDetails> OrdersDetails { get; set; }
    }
}

