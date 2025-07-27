using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SD_Burger.Core.Entities
{
    public class Order : BaseEntity
    {
        public string OrderNumber { get; set; } = string.Empty;
        public DateTime OrderDate { get; set; }
        public OrderStatus Status { get; set; }
        public OrderPriority Priority { get; set; }
        public decimal TotalAmount { get; set; }
        public string CustomerName { get; set; } = string.Empty;
        public string CustomerPhone { get; set; } = string.Empty;
        public string Notes { get; set; } = string.Empty;
        public int TableId { get; set; }
        public int BranchId { get; set; }
        public int? WaiterId { get; set; }
        public virtual Table? Table { get; set; }
        public virtual Branch? Branch { get; set; }
        public virtual User? Waiter { get; set; }
        public virtual List<OrderItem> OrderItems { get; set; } = new();
        public virtual Payment? Payment { get; set; }
    }

    public enum OrderStatus
    {
        [Display(Name = "Alındı")]
        Received,
        [Display(Name = "Hazırlanıyor")]
        Preparing,
        [Display(Name = "Hazır")]
        Ready,
        [Display(Name = "Servis Edildi")]
        Served,
        [Display(Name = "Tamamlandı")]
        Completed,
        [Display(Name = "İptal Edildi")]
        Cancelled
    }

    public enum OrderPriority
    {
        [Display(Name = "Normal")]
        Normal,
        [Display(Name = "Acil")]
        Urgent
    }
} 