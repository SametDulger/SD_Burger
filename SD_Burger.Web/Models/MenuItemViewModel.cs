namespace SD_Burger.Web.Models
{
    public class MenuItemViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public string ImageUrl { get; set; } = string.Empty;
        public bool IsAvailable { get; set; }
        public bool IsActive { get; set; } // Alias for IsAvailable
        public int CategoryId { get; set; }
        public string CategoryName { get; set; } = string.Empty;
        public DateTime? CreatedDate { get; set; }
        public DateTime? CreatedAt { get; set; } // Alias for CreatedDate
        public DateTime? UpdatedAt { get; set; }
    }

    public class CreateMenuItemViewModel
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public string ImageUrl { get; set; } = string.Empty;
        public bool IsAvailable { get; set; }
        public bool IsActive { get; set; } = true; // Alias for IsAvailable
        public int CategoryId { get; set; }
    }

    public class UpdateMenuItemViewModel
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public string ImageUrl { get; set; } = string.Empty;
        public bool IsAvailable { get; set; }
        public bool IsActive { get; set; } // Alias for IsAvailable
        public int CategoryId { get; set; }
    }
} 