using System;
using System.Text.Json.Serialization;
using SD_Burger.Core.Entities;

namespace SD_Burger.Application.DTOs
{
    public class PaymentDto
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public string OrderNumber { get; set; }
        public decimal Amount { get; set; }
        
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public PaymentMethod PaymentMethod { get; set; }
        
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public PaymentStatus Status { get; set; }
        
        public DateTime PaymentDate { get; set; }
        public string TransactionId { get; set; }
        public string Notes { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
    }

    public class CreatePaymentDto
    {
        public int OrderId { get; set; }
        public decimal Amount { get; set; }
        
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public PaymentMethod PaymentMethod { get; set; }
        
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public PaymentStatus Status { get; set; }
        
        public DateTime PaymentDate { get; set; }
        public string TransactionId { get; set; }
        public string Notes { get; set; }
    }

    public class UpdatePaymentDto
    {
        public decimal Amount { get; set; }
        
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public PaymentMethod PaymentMethod { get; set; }
        
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public PaymentStatus Status { get; set; }
        
        public DateTime PaymentDate { get; set; }
        public string TransactionId { get; set; }
        public string Notes { get; set; }
        public bool IsActive { get; set; }
    }
} 