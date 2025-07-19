namespace SD_Burger.Application.DTOs
{
    public class InventoryReportDto
    {
        public int TotalItems { get; set; }
        public int TotalIngredients { get; set; }
        public int LowStockItems { get; set; }
        public int SufficientStock { get; set; }
        public int LowStock { get; set; }
        public int OutOfStockItems { get; set; }
        public int OutOfStock { get; set; }
        public decimal TotalValue { get; set; }
        public List<InventoryDto> LowStockInventory { get; set; } = new();
        public List<InventoryDto> OutOfStockInventory { get; set; } = new();
        public List<BranchStockDataDto> BranchStockData { get; set; } = new();
    }

    public class BranchStockDataDto
    {
        public string BranchName { get; set; } = string.Empty;
        public int TotalItems { get; set; }
        public int LowStockItems { get; set; }
        public int OutOfStockItems { get; set; }
    }
} 