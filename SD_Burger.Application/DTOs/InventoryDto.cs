using System;

namespace SD_Burger.Application.DTOs
{
    public class InventoryDto
    {
        public int Id { get; set; }
        public int IngredientId { get; set; }
        public string? IngredientName { get; set; }
        public int BranchId { get; set; }
        public string? BranchName { get; set; }
        public decimal CurrentStock { get; set; }
        public DateTime LastUpdated { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
    }

    public class CreateInventoryDto
    {
        public int IngredientId { get; set; }
        public int BranchId { get; set; }
        public decimal CurrentStock { get; set; }
    }

    public class UpdateInventoryDto
    {
        public int IngredientId { get; set; }
        public int BranchId { get; set; }
        public decimal CurrentStock { get; set; }
        public bool IsActive { get; set; }
    }
} 