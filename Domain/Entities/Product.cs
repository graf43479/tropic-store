using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Domain.Entities
{
    public class Product
    {
        [HiddenInput(DisplayValue=false)]
        //[Range(1, int.MaxValue, ErrorMessage = "Некорректное число")]
        public int ProductID { get; set; }
        [Required(ErrorMessage = "Пожалуйста, введите название товара")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Пожалуйста, введите описание товара")]
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }
        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "Введите положительное число")]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "Пожалуйста, введите артикул товара")]
        public string ArticleNumber { get; set; }

        [Required(ErrorMessage = "Пожалуйста, введите количество товара на складе")]
        public int Quantity { get; set ; }

        public int Sequence { get; set; }

        // [Required(ErrorMessage = "Пожалуйста, укажите категорию товара")]
      //  public string Category { get; set; }
/*        
        public byte ImageData { get; set; }
        [HiddenInput(DisplayValue = false)]
        public string ImageMimeType { get; set; } 
        */
        //[HiddenInput(DisplayValue = false)]
        public int CategoryID { get; set; }

        public string ShortName { get; set; }

        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}", ApplyFormatInEditMode = true)] //не работает

        public DateTime UpdateDate { get; set; }

        public bool? IsActive { get; set; }

        public bool? IsDeleted { get; set; }

        [Range(0.01, double.MaxValue, ErrorMessage = "Введите положительное число")]
        public decimal? OldPrice { get; set; }

        public DateTime? LastPriceChangeDate { get; set; }

        public string Keywords { get; set; }

        public string Snippet { get; set; }

        public virtual string CategoryName {
            get { return Category.Name; }
            //set { }
        }

        //[ForeignKey("CategoryID")]
        public virtual Category Category { get; set; }
        
        public virtual ICollection<OrderDetails> OrdersDetails { get; set; }

        public virtual ICollection<ProductImage> ProductImages { get; set; }
    }
}
