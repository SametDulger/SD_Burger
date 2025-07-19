namespace SD_Burger.Application.DTOs
{
    public class SalesReportDto
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public decimal TotalSales { get; set; }
        public int TotalOrders { get; set; }
        public decimal AverageOrderValue { get; set; }
        public int ActiveOrders { get; set; }
        public List<TopSellingItemDto> TopSellingItems { get; set; } = new();
        public List<OrderDto> RecentOrders { get; set; } = new();
    }

    public class TopSellingItemDto
    {
        public string Name { get; set; } = string.Empty;
        public int Quantity { get; set; }
        public decimal TotalSales { get; set; }
    }
} 