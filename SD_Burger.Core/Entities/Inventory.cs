using System;

namespace SD_Burger.Core.Entities
{
    public class Inventory : BaseEntity
    {
        public int IngredientId { get; set; }
        public int BranchId { get; set; }
        public decimal CurrentStock { get; set; }
        public decimal Quantity { get; set; }
        public string Unit { get; set; } = string.Empty;
        public decimal MinimumStock { get; set; }
        public decimal Price { get; set; }
        public DateTime LastUpdated { get; set; }
        public bool IsActive { get; set; } = true;
        public DateTime UpdatedDate { get; set; }
        public virtual Ingredient Ingredient { get; set; }
        public virtual Branch Branch { get; set; }
    }
} 