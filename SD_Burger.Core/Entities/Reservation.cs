using System;
using System.ComponentModel.DataAnnotations;

namespace SD_Burger.Core.Entities
{
    public class Reservation : BaseEntity
    {
        public DateTime ReservationDate { get; set; }
        public TimeSpan ReservationTime { get; set; }
        public int GuestCount { get; set; }
        public string CustomerName { get; set; } = string.Empty;
        public string CustomerPhone { get; set; } = string.Empty;
        public string CustomerEmail { get; set; } = string.Empty;
        public string SpecialRequests { get; set; } = string.Empty;
        public ReservationStatus Status { get; set; }
        public int TableId { get; set; }
        public int BranchId { get; set; }
        public int? UserId { get; set; }
        public virtual Table? Table { get; set; }
        public virtual Branch? Branch { get; set; }
        public virtual User? User { get; set; }
    }

    public enum ReservationStatus
    {
        [Display(Name = "Beklemede")]
        Pending,
        [Display(Name = "Onaylandı")]
        Confirmed,
        [Display(Name = "İptal Edildi")]
        Cancelled,
        [Display(Name = "Tamamlandı")]
        Completed
    }
} 