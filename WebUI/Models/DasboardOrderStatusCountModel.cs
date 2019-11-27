

namespace WebUI.Models
{
    public class DasboardOrderStatusCountModel
    {
        public string DimOrderStatusDesc { get; set; }
        public int AllTimeCount { get; set; }
        public int YaerlyCount { get; set; }
        public int MonthlyCount { get; set; }
        public int WeeklyCount { get; set; }
        public int DailyCount { get; set; }
    }
}