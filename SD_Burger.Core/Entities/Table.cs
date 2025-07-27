using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SD_Burger.Core.Entities
{
    public class Table : BaseEntity
    {
        public int TableNumber { get; set; }
        public int Capacity { get; set; }
        public TableStatus Status { get; set; }
        public int BranchId { get; set; }
        public virtual Branch? Branch { get; set; }
        public virtual List<Reservation> Reservations { get; set; } = new();
        public virtual List<Order> Orders { get; set; } = new();
    }

    public enum TableStatus
    {
        [Display(Name = "Müsait")]
        Available,
        [Display(Name = "Dolu")]
        Occupied,
        [Display(Name = "Rezerve")]
        Reserved,
        [Display(Name = "Servis Dışı")]
        OutOfService
    }
} 