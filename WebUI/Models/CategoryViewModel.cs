using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Domain.Entities;

namespace WebUI.Models
{
    public class CategoryViewModel
    {
        public int CategoryID { get; set; }

        [Required(ErrorMessage = "Please enter a category name")]
        public string Name { get; set; }

        public string ShortName { get; set; }

        public string ImageExt { get; set; }

        public int Sequence { get; set; }

        public bool IsDeleted { get; set; }


        //public virtual ICollection<Product> Products { get; set; }

        public IEnumerable<SuperCategory> SuperCategories { get; set; }

        public int SuperCategoryID { get; set; }
        /*
        public virtual string SuperCategoryName
        {
            get { return SuperCategory.Name; }
            //set { }
        }
        */
        public string SuperCategoryName { get; set; }

        public int SelectedSuperCategoryID { get; set; }


        public string CategoryKeywords { get; set; }

        public string CategorySnippet { get; set; }

        public string Description { get; set; }

        public DateTime UpdateDate { get; set; }

        //public virtual SuperCategory SuperCategory { get; set; }

    }
}