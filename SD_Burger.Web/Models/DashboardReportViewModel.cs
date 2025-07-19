namespace SD_Burger.Web.Models
{
    public class DashboardReportViewModel
    {
        public decimal TotalSales { get; set; }
        public decimal TotalRevenue { get; set; }
        public int TotalOrders { get; set; }
        public int TotalUsers { get; set; }
        public int TotalBranches { get; set; }
        public int ActiveReservations { get; set; }
        public int CompletedOrders { get; set; }
        public int PreparingOrders { get; set; }
        public int PendingOrders { get; set; }
        public int CancelledOrders { get; set; }
        public List<TopSellingItemViewModel> TopSellingItems { get; set; } = new();
        public List<TopSellingItemViewModel> TopMenuItems { get; set; } = new();
        public List<BranchPerformanceViewModel> BranchPerformance { get; set; } = new();
        public List<MonthlySalesViewModel> MonthlySales { get; set; } = new();
        public List<MonthlyRevenueData> MonthlyRevenue { get; set; } = new();
        public List<OrderViewModel> RecentOrders { get; set; } = new();
    }

    public class BranchPerformanceViewModel
    {
        public string BranchName { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public decimal Sales { get; set; }
        public int Orders { get; set; }
        public int OrderCount { get; set; }
        public int Reservations { get; set; }
    }

    public class MonthlySalesViewModel
    {
        public string Month { get; set; } = string.Empty;
        public decimal Sales { get; set; }
        public decimal Amount { get; set; }
        public int Orders { get; set; }
    }

    public class TopSellingItemViewModel
    {
        public string Name { get; set; } = string.Empty;
        public int Quantity { get; set; }
        public decimal Revenue { get; set; }
        public decimal TotalRevenue { get; set; }
        public decimal TotalSales { get; set; }
    }
} 