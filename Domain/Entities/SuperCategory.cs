using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace Domain.Entities
{
    public class SuperCategory
    {
         public int SuperCategoryID { get; set; }

        [Required (ErrorMessage = "Введите название надкатегории")]
        public string Name { get; set; }

        public string ShortName { get; set; }

        public string ImageExt { get; set; }

        public int Sequence { get; set; }

        public virtual ICollection<Category> Categories { get; set; }

        //public virtual OrderDetails OrderDetails { get; set; }
        
       // public virtual ICollection<OrderDetails> OrdersDetails { get; set; }
    }
}
