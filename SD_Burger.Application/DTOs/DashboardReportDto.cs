namespace SD_Burger.Application.DTOs
{
    public class DashboardReportDto
    {
        public decimal TotalSales { get; set; }
        public int TotalOrders { get; set; }
        public int ActiveUsers { get; set; }
        public int LowStockItems { get; set; }
        public int ActiveReservations { get; set; }
        public int CompletedOrders { get; set; }
        public int PreparingOrders { get; set; }
        public int PendingOrders { get; set; }
        public int CancelledOrders { get; set; }
        public List<TopSellingItemDto> TopSellingItems { get; set; } = new();
        public List<BranchPerformanceDto> BranchPerformance { get; set; } = new();
        public List<MonthlySalesDto> MonthlySales { get; set; } = new();
    }

    public class BranchPerformanceDto
    {
        public string BranchName { get; set; } = string.Empty;
        public decimal Sales { get; set; }
        public int Orders { get; set; }
        public int Reservations { get; set; }
    }

    public class MonthlySalesDto
    {
        public string Month { get; set; } = string.Empty;
        public decimal Sales { get; set; }
        public int Orders { get; set; }
    }
} 