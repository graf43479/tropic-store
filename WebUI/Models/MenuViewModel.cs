using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Domain.Entities;

namespace WebUI.Models
{
    public class MenuViewModel
    {
        public SuperCategory SuperCategory { get; set; }
        public IEnumerable<Category> Categories { get; set; }
      //  public IEnumerable<Product> Products { get; set; }
    }
}