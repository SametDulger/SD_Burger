namespace SD_Burger.Web.Models
{
    public class InventoryReportViewModel
    {
        public int TotalItems { get; set; }
        public int TotalIngredients { get; set; }
        public int LowStockItems { get; set; }
        public int SufficientStock { get; set; }
        public int LowStock { get; set; }
        public int OutOfStockItems { get; set; }
        public int OutOfStock { get; set; }
        public decimal TotalValue { get; set; }
        public decimal InventoryValue { get; set; }
        public List<InventoryViewModel> LowStockInventory { get; set; } = new();
        public List<InventoryViewModel> OutOfStockInventory { get; set; } = new();
        public List<BranchStockDataViewModel> BranchStockData { get; set; } = new();
    }

    public class BranchStockDataViewModel
    {
        public string BranchName { get; set; } = string.Empty;
        public int TotalItems { get; set; }
        public int LowStockItems { get; set; }
        public int OutOfStockItems { get; set; }
        public int TotalStock { get; set; }
    }
} 