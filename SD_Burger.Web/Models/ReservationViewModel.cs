namespace SD_Burger.Web.Models
{
    public class ReservationViewModel
    {
        public int Id { get; set; }
        public string ReservationNumber { get; set; } = string.Empty;
        public DateTime? ReservationDate { get; set; }
        public TimeSpan? ReservationTime { get; set; }
        public int GuestCount { get; set; }
        public int NumberOfGuests { get; set; } // Alias for GuestCount
        public string CustomerName { get; set; } = string.Empty;
        public string CustomerPhone { get; set; } = string.Empty;
        public string CustomerEmail { get; set; } = string.Empty;
        public string SpecialRequests { get; set; } = string.Empty;
        public string Notes { get; set; } = string.Empty; // Alias for SpecialRequests
        public string Status { get; set; } = string.Empty;
        public int TableId { get; set; }
        public int TableNumber { get; set; }
        public string TableName { get; set; } = string.Empty;
        public int BranchId { get; set; }
        public string BranchName { get; set; } = string.Empty;
        public int? UserId { get; set; }
        public string UserName { get; set; } = string.Empty;
        public DateTime? CreatedDate { get; set; }
        public DateTime? CreatedAt { get; set; } // Alias for CreatedDate
        public DateTime? UpdatedAt { get; set; }
    }

    public class CreateReservationViewModel
    {
        public DateTime ReservationDate { get; set; }
        public TimeSpan ReservationTime { get; set; }
        public int GuestCount { get; set; }
        public int NumberOfGuests { get; set; } // Alias for GuestCount
        public string CustomerName { get; set; } = string.Empty;
        public string CustomerPhone { get; set; } = string.Empty;
        public string CustomerEmail { get; set; } = string.Empty;
        public string SpecialRequests { get; set; } = string.Empty;
        public string Notes { get; set; } = string.Empty; // Alias for SpecialRequests
        public int TableId { get; set; }
        public int BranchId { get; set; }
        public int? UserId { get; set; }
    }

    public class UpdateReservationViewModel
    {
        public int Id { get; set; }
        public DateTime ReservationDate { get; set; }
        public TimeSpan ReservationTime { get; set; }
        public int GuestCount { get; set; }
        public int NumberOfGuests { get; set; } // Alias for GuestCount
        public string CustomerName { get; set; } = string.Empty;
        public string CustomerPhone { get; set; } = string.Empty;
        public string CustomerEmail { get; set; } = string.Empty;
        public string SpecialRequests { get; set; } = string.Empty;
        public string Notes { get; set; } = string.Empty; // Alias for SpecialRequests
        public string Status { get; set; } = string.Empty;
        public int TableId { get; set; }
        public int BranchId { get; set; }
        public int? UserId { get; set; }
    }
} 