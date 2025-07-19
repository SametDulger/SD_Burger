namespace SD_Burger.Application.DTOs
{
    public class UserReportDto
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int TotalUsers { get; set; }
        public int ActiveUsers { get; set; }
        public int InactiveUsers { get; set; }
        public int NewUsersThisMonth { get; set; }
        public List<UserDto> Users { get; set; } = new();
        public List<UserDto> RecentUsers { get; set; } = new();
        public List<UserActivityDto> UserActivities { get; set; } = new();
        public List<RoleDistributionDto> RoleDistribution { get; set; } = new();
    }

    public class UserActivityDto
    {
        public int UserId { get; set; }
        public string UserName { get; set; } = string.Empty;
        public DateTime LastLoginDate { get; set; }
        public int LoginCount { get; set; }
    }

    public class RoleDistributionDto
    {
        public string Role { get; set; } = string.Empty;
        public int Count { get; set; }
        public double Percentage { get; set; }
    }
} 