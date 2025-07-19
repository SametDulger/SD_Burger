namespace SD_Burger.Web.Models
{
    public class UserViewModel
    {
        public int Id { get; set; }
        public string Username { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty; // Alias for PhoneNumber
        public string Password { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty;
        public int? BranchId { get; set; }
        public string BranchName { get; set; } = string.Empty;
        public DateTime? CreatedDate { get; set; }
        public DateTime? CreatedAt { get; set; } // Alias for CreatedDate
        public DateTime? UpdatedAt { get; set; }
        public DateTime? LastLoginDate { get; set; }
        public bool IsActive { get; set; }
    }

    public class CreateUserViewModel
    {
        public string Username { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty; // Alias for PhoneNumber
        public string Address { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty;
        public int? BranchId { get; set; }
        public bool IsActive { get; set; } = true;
    }

    public class UpdateUserViewModel
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty; // Alias for PhoneNumber
        public string Password { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty;
        public int? BranchId { get; set; }
    }

    public class LoginViewModel
    {
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
} 