using System;
using System.Text.Json.Serialization;
using SD_Burger.Core.Entities;

namespace SD_Burger.Application.DTOs
{
    public class IngredientDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public IngredientUnit Unit { get; set; }
        
        public decimal UnitPrice { get; set; }
        public int MinimumStock { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
    }

    public class CreateIngredientDto
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public IngredientUnit Unit { get; set; }
        
        public decimal UnitPrice { get; set; }
        public int MinimumStock { get; set; }
    }

    public class UpdateIngredientDto
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public IngredientUnit Unit { get; set; }
        
        public decimal UnitPrice { get; set; }
        public int MinimumStock { get; set; }
        public bool IsActive { get; set; }
    }
} 