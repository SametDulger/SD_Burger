using System.Collections.Generic;

namespace SD_Burger.Core.Entities
{
    public class MenuItem : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string ImageUrl { get; set; }
        public bool IsAvailable { get; set; } = true;
        public int CategoryId { get; set; }
        public virtual Category Category { get; set; }
        public virtual ICollection<OrderItem> OrderItems { get; set; }
        public virtual ICollection<MenuItemIngredient> MenuItemIngredients { get; set; }
    }
} 