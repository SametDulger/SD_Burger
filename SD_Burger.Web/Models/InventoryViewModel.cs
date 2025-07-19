namespace SD_Burger.Web.Models
{
    public class InventoryViewModel
    {
        public int Id { get; set; }
        public int IngredientId { get; set; }
        public string IngredientName { get; set; } = string.Empty;
        public int BranchId { get; set; }
        public string BranchName { get; set; } = string.Empty;
        public decimal CurrentStock { get; set; }
        public decimal Quantity { get; set; } // Alias for CurrentStock
        public string Unit { get; set; } = string.Empty;
        public decimal MinimumStock { get; set; }
        public bool IsActive { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? CreatedAt { get; set; } // Alias for CreatedDate
        public DateTime? UpdatedAt { get; set; }
        public DateTime? LastUpdated { get; set; } // Alias for UpdatedAt
    }

    public class CreateInventoryViewModel
    {
        public int IngredientId { get; set; }
        public int BranchId { get; set; }
        public decimal CurrentStock { get; set; }
        public decimal Quantity { get; set; } // Alias for CurrentStock
        public decimal MinimumStock { get; set; }
        public bool IsActive { get; set; } = true;
    }

    public class UpdateInventoryViewModel
    {
        public int Id { get; set; }
        public int IngredientId { get; set; }
        public int BranchId { get; set; }
        public decimal CurrentStock { get; set; }
        public decimal Quantity { get; set; } // Alias for CurrentStock
        public decimal MinimumStock { get; set; }
        public bool IsActive { get; set; }
    }
} 