namespace SD_Burger.Core.Entities
{
    public class MenuItemIngredient : BaseEntity
    {
        public int MenuItemId { get; set; }
        public int IngredientId { get; set; }
        public decimal Quantity { get; set; }
        public virtual MenuItem MenuItem { get; set; }
        public virtual Ingredient Ingredient { get; set; }
    }
} 