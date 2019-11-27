
namespace WebUI.Models
{
    public class CategoryUrlFriendlyModel
    {
        public string Name { get; set; }
        public string ShortName { get; set; }
        public int CategoryID { get; set; }
        public int SuperCategoryID { get; set; }
        public bool? IsDeleted { get; set; }
        public int SuperCategorySequence { get; set; }
        public string SuperCategoryName { get; set; }
    }
}