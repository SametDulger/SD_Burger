using System.Text.Json.Serialization;
using SD_Burger.Core.Entities;

namespace SD_Burger.Application.DTOs
{
    public class UserDto
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public UserRole Role { get; set; }
        
        public int? BranchId { get; set; }
        public string BranchName { get; set; }
        public DateTime CreatedDate { get; set; }
        public bool IsActive { get; set; }
    }

    public class CreateUserDto
    {
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public UserRole Role { get; set; }
        
        public int? BranchId { get; set; }
    }

    public class UpdateUserDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public UserRole Role { get; set; }
        
        public int? BranchId { get; set; }
    }

    public class LoginDto
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
} 