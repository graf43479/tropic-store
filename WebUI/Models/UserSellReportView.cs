
namespace WebUI.Models
{
    public class UserSellReportView
    {
        public string Login { get; set; }
        public decimal PriceSummary { get; set; }
        public int SellCount { get; set; }
        public int OrderPriceAVG { get; set; }
    }
}