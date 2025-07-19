using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SD_Burger.Core.Entities
{
    public class User : BaseEntity
    {
        public string Username { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public UserRole Role { get; set; }
        public int? BranchId { get; set; }
        public virtual Branch Branch { get; set; }
        public virtual ICollection<Reservation> Reservations { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
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