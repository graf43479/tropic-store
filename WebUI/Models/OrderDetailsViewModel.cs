
using Domain.Entities;

namespace WebUI.Models
{
    public class OrderDetailsViewModel
    {
        public int OrderDetailsID { get; set; }
        
        public int ProductID { get; set; }

        public int OrderSummaryID { get; set; }

        public int UserID { get; set; }

        public string CategoryName { get; set; }

        public string ProductName { get; set; }

        public int QuantityInOrder { get; set; }

        public int QuantityInStore { get; set; }

        public decimal PriceInOrder { get; set; }

        public decimal PriceNow { get; set; }

        public decimal TotalPrice { get; set; }

        public OrdersSummary OrdersSummary { get; set; }
    }
}