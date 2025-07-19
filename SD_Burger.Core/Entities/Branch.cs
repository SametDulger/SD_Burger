using System.Collections.Generic;

namespace SD_Burger.Core.Entities
{
    public class Branch : BaseEntity
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public int TableCount { get; set; }

        public virtual ICollection<User> Users { get; set; }
        public virtual ICollection<Table> Tables { get; set; }
        public virtual ICollection<Reservation> Reservations { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
        public virtual ICollection<Inventory> Inventories { get; set; }
    }
} 