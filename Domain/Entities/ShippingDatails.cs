using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace Domain.Entities
{
    public class ShippingDatails
    {
        [Required(ErrorMessage="Пожалуйста введите имя")]
        public string ShippingName { get; set; }
        
        [Required(ErrorMessage = "Пожалуйста введите строку адреса доставки")]
        public string ShippingAddress { get; set; }
 
        [Required(ErrorMessage = "Пожалуйста введите номер телефона для обратной связи")]
        public string ShippingPhone { get; set; }

        [Required(ErrorMessage = "Введите email")]
        [RegularExpression(@"^[a-zA-Z0-9.-]{1,20}@[a-zA-Z0-9.-]{1,20}\.[A-Za-z]{2,4}", ErrorMessage = "Неверный формат Email")]
        public string ShippingEmail { get; set; }
        //public string ShippingCapcha { get; set; } ????!!!!!!!!!!!!
        public bool GiftWrap { get; set; }

        [Required(ErrorMessage = "Выберите вариант доставки")]
        public int DimShippingID { get; set; }

        public DimShipping DimShipping { get; set; }

        public IEnumerable<DimShipping> DimShippings { get; set; } 
        //public string ShippingEmail { get; set; }
    }
}
