using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SD_Burger.Core.Entities
{
    public class Ingredient : BaseEntity
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public IngredientUnit Unit { get; set; }
        public decimal UnitPrice { get; set; }
        public int MinimumStock { get; set; }
        public virtual List<MenuItemIngredient> MenuItemIngredients { get; set; } = new();
        public virtual List<Inventory> Inventories { get; set; } = new();
    }

    public enum IngredientUnit
    {
        [Display(Name = "Kilogram")]
        Kilogram,
        [Display(Name = "Gram")]
        Gram,
        [Display(Name = "Litre")]
        Liter,
        [Display(Name = "Mililitre")]
        Milliliter,
        [Display(Name = "Adet")]
        Piece,
        [Display(Name = "Paket")]
        Package,
        [Display(Name = "Kutu")]
        Box,
        [Display(Name = "Şişe")]
        Bottle
    }
} 