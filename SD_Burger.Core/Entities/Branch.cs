using System.Collections.Generic;

namespace SD_Burger.Core.Entities
{
    public class Branch : BaseEntity
    {
        public string Name { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public int TableCount { get; set; }

        public virtual List<User> Users { get; set; } = new();
        public virtual List<Table> Tables { get; set; } = new();
        public virtual List<Reservation> Reservations { get; set; } = new();
        public virtual List<Order> Orders { get; set; } = new();
        public virtual List<Inventory> Inventories { get; set; } = new();
    }
} 