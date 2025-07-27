using System;
using System.ComponentModel.DataAnnotations;

namespace SD_Burger.Core.Entities
{
    public class Payment : BaseEntity
    {
        public int OrderId { get; set; }
        public decimal Amount { get; set; }
        public PaymentMethod PaymentMethod { get; set; }
        public PaymentStatus Status { get; set; }
        public DateTime PaymentDate { get; set; }
        public string TransactionId { get; set; } = string.Empty;
        public string Notes { get; set; } = string.Empty;
        public virtual Order? Order { get; set; }
    }

    public enum PaymentMethod
    {
        [Display(Name = "Nakit")]
        Cash,
        [Display(Name = "Kredi Kartı")]
        CreditCard,
        [Display(Name = "Banka Kartı")]
        DebitCard,
        [Display(Name = "Online Ödeme")]
        OnlinePayment
    }

    public enum PaymentStatus
    {
        [Display(Name = "Beklemede")]
        Pending,
        [Display(Name = "Tamamlandı")]
        Completed,
        [Display(Name = "Başarısız")]
        Failed,
        [Display(Name = "İade Edildi")]
        Refunded
    }
} 