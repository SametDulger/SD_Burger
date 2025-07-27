using System.Collections.Generic;

namespace SD_Burger.Core.Entities
{
    public class MenuItem : BaseEntity
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public string ImageUrl { get; set; } = string.Empty;
        public bool IsAvailable { get; set; } = true;
        public int CategoryId { get; set; }
        public virtual Category? Category { get; set; }
        public virtual List<OrderItem> OrderItems { get; set; } = new();
        public virtual List<MenuItemIngredient> MenuItemIngredients { get; set; } = new();
    }
} 