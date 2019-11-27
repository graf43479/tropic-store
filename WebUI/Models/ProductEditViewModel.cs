using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using Domain.Entities;

namespace WebUI.Models
{
   // [OutputCache]
    public class ProductEditViewModel
    {
        [HiddenInput(DisplayValue = false)]
        public int ProductID { get; set; }

        [Required(ErrorMessage = "Введите название товара")]
        public string Name { get; set; }

        [DataType(DataType.MultilineText)]
        [Required(ErrorMessage = "Необходимо описание")]
        public string Description { get; set; }

        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "Введите положительное число")]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "Пожалуйста, введите артикул товара")]
        public string ArticleNumber { get; set; }

        [Required(ErrorMessage = "Укажите количество товара в наличии")]
        public int Quantity { get; set; }

        public string ShortName { get; set; }

        public int Sequence { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime UpdateDate { get; set; }
        
        public string CategoryName { get; set; }

        public int SelectedCategoryID { get; set; }

        public IEnumerable<Category> Categories { get; set; }

        public IEnumerable<ProductImage> ProductImages { get; set; }

        public bool IsInStock { get; set; }

        [Required]
        public bool? IsActive { get; set; }
        [Required]
        public bool? IsDeleted { get; set; }

        [Range(0.01, double.MaxValue, ErrorMessage = "Введите положительное число")]
        public decimal? OldPrice { get; set; }

        public DateTime? LastPriceChangeDate { get; set; }

        public string Keywords { get; set; }

        public string Snippet { get; set; }

      //  public bool FixChangePrice { get; set; }
    }
}