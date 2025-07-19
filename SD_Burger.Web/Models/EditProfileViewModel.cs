using System.ComponentModel.DataAnnotations;

namespace SD_Burger.Web.Models
{
    public class EditProfileViewModel
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty;
        public int? BranchId { get; set; }
    }
} 