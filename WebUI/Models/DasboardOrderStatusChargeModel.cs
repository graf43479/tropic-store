
namespace WebUI.Models
{
    public class DasboardOrderStatusChargeModel
    {
        public string DimOrderStatusDesc { get; set; }
        public decimal AllTimeCharge { get; set; }
        public decimal YaerlyCharge { get; set; }
        public decimal MonthlyCharge { get; set; }
        public decimal WeeklyCharge { get; set; }
        public decimal DailyCharge { get; set; }
    }
}