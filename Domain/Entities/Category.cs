using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;



namespace Domain.Entities
{
    public class Category 
    {
        //[HiddenInput(DisplayValue = false)]
        //[Key]
        public int CategoryID { get; set; }

        [Required (ErrorMessage = "Введите название категории")]
        public string Name { get; set; }

        public string ShortName { get; set; }

        public string ImageExt { get; set; }

        public int Sequence { get; set; }

       // [Required]
        public bool IsDeleted { get; set; }

        public virtual ICollection<Product> Products { get; set; }

        //[ForeignKey("SuperCategory")]
        public int SuperCategoryID { get; set; }

        //public virtual string SuperCategoryName { get { return SuperCategory.Name; } }

        public virtual SuperCategory SuperCategory { get; set; }

        public string CategoryKeywords { get; set; }

        public string CategorySnippet { get; set; }

        public string Description { get; set; }

        
        public DateTime UpdateDate { get; set; }
        //public virtual OrderDetails OrderDetails { get; set; }
        
       // public virtual ICollection<OrderDetails> OrdersDetails { get; set; }
    }
}
