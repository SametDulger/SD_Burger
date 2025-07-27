namespace SD_Burger.Core.Entities
{
    public class OrderItem : BaseEntity
    {
        public int OrderId { get; set; }
        public int MenuItemId { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal TotalPrice { get; set; }
        public string SpecialInstructions { get; set; } = string.Empty;
        public virtual Order? Order { get; set; }
        public virtual MenuItem? MenuItem { get; set; }
    }
} 