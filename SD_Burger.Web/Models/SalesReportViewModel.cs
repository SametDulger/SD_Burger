namespace SD_Burger.Web.Models
{
    public class SalesReportViewModel
    {
        public decimal TotalSales { get; set; }
        public int TotalOrders { get; set; }
        public decimal AverageOrderValue { get; set; }
        public int ActiveOrders { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public List<TopSellingItemViewModel> TopSellingItems { get; set; } = new();
        public List<OrderViewModel> RecentOrders { get; set; } = new();
        public List<SalesData> SalesData { get; set; } = new();
    }
} 