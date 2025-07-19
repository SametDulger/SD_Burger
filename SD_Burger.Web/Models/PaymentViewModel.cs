namespace SD_Burger.Web.Models
{
    public class PaymentViewModel
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public string OrderNumber { get; set; } = string.Empty;
        public string PaymentNumber { get; set; } = string.Empty;
        public decimal Amount { get; set; }
        public decimal OrderAmount { get; set; }
        public string PaymentMethod { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
        public string TransactionId { get; set; } = string.Empty;
        public string Notes { get; set; } = string.Empty;
        public string CustomerName { get; set; } = string.Empty;
        public string BranchName { get; set; } = string.Empty;
        public DateTime? PaymentDate { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? CreatedAt { get; set; } // Alias for CreatedDate
        public DateTime? UpdatedAt { get; set; }
    }

    public class CreatePaymentViewModel
    {
        public int OrderId { get; set; }
        public decimal Amount { get; set; }
        public string PaymentMethod { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
        public string TransactionId { get; set; } = string.Empty;
        public string Notes { get; set; } = string.Empty;
        public DateTime PaymentDate { get; set; }
    }

    public class UpdatePaymentViewModel
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public decimal Amount { get; set; }
        public string PaymentMethod { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
        public string TransactionId { get; set; } = string.Empty;
        public string Notes { get; set; } = string.Empty;
        public DateTime PaymentDate { get; set; }
    }
} 