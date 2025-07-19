namespace SD_Burger.Web.Models
{
    public class OrderViewModel
    {
        public int Id { get; set; }
        public string OrderNumber { get; set; } = string.Empty;
        public DateTime? OrderDate { get; set; }
        public string Status { get; set; } = string.Empty;
        public string Priority { get; set; } = string.Empty;
        public decimal TotalAmount { get; set; }
        public string CustomerName { get; set; } = string.Empty;
        public string CustomerPhone { get; set; } = string.Empty;
        public string Notes { get; set; } = string.Empty;
        public int TableId { get; set; }
        public int TableNumber { get; set; }
        public string TableName { get; set; } = string.Empty;
        public string OrderType { get; set; } = string.Empty;
        public int BranchId { get; set; }
        public string BranchName { get; set; } = string.Empty;
        public int? WaiterId { get; set; }
        public string WaiterName { get; set; } = string.Empty;
        public DateTime? CreatedDate { get; set; }
        public DateTime? CreatedAt { get; set; } // Alias for CreatedDate
        public DateTime? UpdatedAt { get; set; }
        public List<OrderItemViewModel> OrderItems { get; set; } = new();
    }

    public class CreateOrderViewModel
    {
        public string Priority { get; set; } = string.Empty;
        public string CustomerName { get; set; } = string.Empty;
        public string CustomerPhone { get; set; } = string.Empty;
        public string Notes { get; set; } = string.Empty;
        public string OrderType { get; set; } = string.Empty;
        public int TableId { get; set; }
        public int BranchId { get; set; }
        public int? WaiterId { get; set; }
        public List<CreateOrderItemViewModel> OrderItems { get; set; } = new();
    }

    public class UpdateOrderViewModel
    {
        public int Id { get; set; }
        public string Status { get; set; } = string.Empty;
        public string Priority { get; set; } = string.Empty;
        public string CustomerName { get; set; } = string.Empty;
        public string CustomerPhone { get; set; } = string.Empty;
        public string Notes { get; set; } = string.Empty;
        public string OrderType { get; set; } = string.Empty;
        public int TableId { get; set; }
        public int BranchId { get; set; }
        public int UserId { get; set; }
        public int? WaiterId { get; set; }
    }

    public class OrderItemViewModel
    {
        public int Id { get; set; }
        public int MenuItemId { get; set; }
        public string MenuItemName { get; set; } = string.Empty;
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal TotalPrice { get; set; }
        public string SpecialInstructions { get; set; } = string.Empty;
    }

    public class CreateOrderItemViewModel
    {
        public int MenuItemId { get; set; }
        public int Quantity { get; set; }
        public string SpecialInstructions { get; set; } = string.Empty;
    }
} 