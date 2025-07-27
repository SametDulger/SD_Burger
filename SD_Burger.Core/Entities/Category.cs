using System.Collections.Generic;

namespace SD_Burger.Core.Entities
{
    public class Category : BaseEntity
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string ImageUrl { get; set; } = string.Empty;
        public int DisplayOrder { get; set; }
        public virtual List<MenuItem> MenuItems { get; set; } = new();
    }
} 