using System.Collections.Generic;

namespace SD_Burger.Core.Entities
{
    public class Category : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public int DisplayOrder { get; set; }
        public virtual ICollection<MenuItem> MenuItems { get; set; }
    }
} 