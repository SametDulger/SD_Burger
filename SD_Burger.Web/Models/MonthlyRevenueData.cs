namespace SD_Burger.Web.Models
{
    public class MonthlyRevenueData
    {
        public string Month { get; set; } = string.Empty;
        public decimal Revenue { get; set; }
        public int Orders { get; set; }
    }
} 