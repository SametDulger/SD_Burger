using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SD_Burger.Core.Entities
{
    public class User : BaseEntity
    {
        public string Username { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public UserRole Role { get; set; }
        public int? BranchId { get; set; }
        public virtual Branch? Branch { get; set; }
        public virtual List<Reservation> Reservations { get; set; } = new();
        public virtual List<Order> Orders { get; set; } = new();
    }

    public enum UserRole
    {
        [Display(Name = "Yönetici")]
        Admin,
        [Display(Name = "Garson")]
        Waiter,
        [Display(Name = "Mutfak Personeli")]
        KitchenStaff,
        [Display(Name = "Rezervasyon Sorumlusu")]
        ReservationManager,
        [Display(Name = "Muhasebeci")]
        Accountant,
        [Display(Name = "Müşteri")]
        Customer
    }
} 