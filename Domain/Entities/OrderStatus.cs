
namespace Domain.Entities
{
    public class OrderStatus
    {
        public int OrderStatusID { get; set; }
        public int DimOrderStatusID { get; set; }
        public int OrderSummaryID { get; set; }

        public virtual OrdersSummary OrdersSummary { get; set; }
        public virtual DimOrderStatus DimOrderStatus { get; set; }

    }
}
