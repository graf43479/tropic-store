
using System.Collections.Generic;


namespace WebUI.Models
{
    public class SuperCategoriesViewModel
    {
        public IEnumerable<CategoryUrlFriendlyModel> categories { get; set; }
        public string superCategory { get; set; }
        public int Sequence { get; set; }
    }
}