using System.Text.Json.Serialization;
using SD_Burger.Core.Entities;

namespace SD_Burger.Application.DTOs
{
    public class OrderDto
    {
        public int Id { get; set; }
        public string OrderNumber { get; set; }
        public DateTime OrderDate { get; set; }
        
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public OrderStatus Status { get; set; }
        
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public OrderPriority Priority { get; set; }
        
        public decimal TotalAmount { get; set; }
        public string CustomerName { get; set; }
        public string CustomerPhone { get; set; }
        public string Notes { get; set; }
        public int TableId { get; set; }
        public int TableNumber { get; set; }
        public int BranchId { get; set; }
        public string BranchName { get; set; }
        public int? WaiterId { get; set; }
        public string WaiterName { get; set; }
        public DateTime CreatedDate { get; set; }
        public List<OrderItemDto> OrderItems { get; set; }
    }

    public class CreateOrderDto
    {
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public OrderPriority Priority { get; set; }
        
        public string CustomerName { get; set; }
        public string CustomerPhone { get; set; }
        public string Notes { get; set; }
        public int TableId { get; set; }
        public int BranchId { get; set; }
        public int? WaiterId { get; set; }
        public List<CreateOrderItemDto> OrderItems { get; set; }
    }

    public class UpdateOrderDto
    {
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public OrderStatus Status { get; set; }
        
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public OrderPriority Priority { get; set; }
        
        public string CustomerName { get; set; }
        public string CustomerPhone { get; set; }
        public string Notes { get; set; }
        public int? WaiterId { get; set; }
    }

    public class OrderItemDto
    {
        public int Id { get; set; }
        public int MenuItemId { get; set; }
        public string MenuItemName { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal TotalPrice { get; set; }
        public string SpecialInstructions { get; set; }
    }

    public class CreateOrderItemDto
    {
        public int MenuItemId { get; set; }
        public int Quantity { get; set; }
        public string SpecialInstructions { get; set; }
    }
} 